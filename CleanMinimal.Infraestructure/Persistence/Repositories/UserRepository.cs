using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanMinimal.Infraestructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public Task<List<User>> GetAll(string query) => _context.Users.Where(u => (u.Name + " " + u.LastName).Contains(query)).ToListAsync();
    public async Task<User?> SelectById(BaseId Id) => await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
    public void Create(User user) => _context.Users.Add(user);
    public void Update(User user) => _context.Users.Update(user);
    public void Delete(BaseId Id) => _context.Users.Where(u => u.Id == Id).ExecuteDeleteAsync();
    public Task<bool> AlredyExists(BaseId Id) => _context.Users.AnyAsync(u => u.Id == Id);
}
