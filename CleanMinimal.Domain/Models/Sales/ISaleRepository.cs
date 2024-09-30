using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;

namespace CleanMinimal.Domain.Models;

public interface ISaleRepository
{
    Task<List<Sale>> GetAll(string query);
    Task<Sale> GetAllByUserId(BaseId UserId);
    void Create(Sale sale);
}