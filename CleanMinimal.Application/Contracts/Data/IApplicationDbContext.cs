using CleanMinimal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanMinimal.Application.Contracts.Data;

public interface IApplicationDbcontext
{
    DbSet<User> Users { get; set; }
    DbSet<Sale> Sales { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}