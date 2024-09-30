using CleanMinimal.Application.Contracts.Data;
using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanMinimal.Infraestructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbcontext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public DbSet<User> Users { get ; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.sales)
            .WithOne(s => s.user)
            .HasForeignKey(s => s.UserId);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e => e.GetDomainEvents());
        
        ChangeTracker.Entries()
            .Where(e => e.Entity is BaseAuditableModel && 
                    (
                        e.State == EntityState.Added ||
                        e.State == EntityState.Modified
                    ))
            .ToList()
            .ForEach(entry =>
            {
                var entity = (BaseAuditableModel)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entity.CreatedDate == default)
                        {
                            entity.CreatedDate = DateTime.Now;
                        }
                        break;
                    case EntityState.Modified:
                        Entry(entity)
                            .Property(m => m.CreatedDate).IsModified = false;
                        entity.UpdatedDate = DateTime.Now;
                        break;
                    default:
                        break;
                }
                // if (entry.State == EntityState.Added)
                // {
                //     entity.CreatedDate = DateTime.UtcNow;
                // }
                entity.UpdatedDate = DateTime.UtcNow;
            });

        
        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
        
        return result;
    }
}
