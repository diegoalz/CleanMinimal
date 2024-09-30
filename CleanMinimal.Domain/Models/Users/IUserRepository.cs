using CleanMinimal.Domain.Common;

namespace CleanMinimal.Domain.Models;

public interface IUserRepository
{
    Task<List<User>> GetAll(string query);
    Task<User?> SelectById(BaseId Id);
    void Create(User user);
    void Update(User user);
    void Delete(BaseId Id);
    Task<bool> AlredyExists(BaseId Id);
}
