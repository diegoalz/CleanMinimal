using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanMinimal.Infraestructure.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public void Create(Sale sale) => _context.Sales.Add(sale);

    public Task<List<Sale>> GetAll(string query) => _context.Sales.ToListAsync();

    public Task<Sale> GetAllByUserId(BaseId UserId)
    {
        throw new NotImplementedException();
    }
}