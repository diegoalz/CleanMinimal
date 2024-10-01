using CleanMinimal.Domain.Common;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.ValueObjects;

namespace CleanMinimal.Domain.Models;

public sealed class User : BaseAuditableModel
{
    public User(
            BaseId id,
            string name,
            string lastName,
            Email email,
            PhoneNumber phoneNumber,
            bool active = true
        ): base(id)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Active = active;
    }
    protected User(BaseId id): base(id)
    {
        
    }

    public static User Update(
        Guid id,
        string name,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        bool active = true
    )
    {
        return new User(new BaseId(id), name, lastName, email, phoneNumber, active);
    }
    public string Name { get; set; }
    public string LastName { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public bool Active { get; set; } = true;
    public string FullName => $"{Name} {LastName}";

    public List<Sale> sales { get; set; } = new List<Sale>();
}
