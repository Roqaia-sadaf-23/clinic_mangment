using Clinic_Domain.Entities;

namespace Clinic_Domain.Entities;

public class User
{
    private User() { }

    protected User(
        Person person,
        string email,
        string userName,
        string passwordHash,
        bool isActive)
    {
        Person = person;
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; private set; }

    public int PersonId { get; private set; }
    public virtual Person Person { get; private set; } = null!;

    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get;  set; } = new List<RefreshToken>();
    public virtual ICollection<UserRole> UserRoles { get;  set; } = new List<UserRole>();

    public static User CreateUser(
        Person person,
        string email,
        string userName,
        string passwordHash,
        bool isActive)
    {
        return new User(person, email, userName, passwordHash, isActive);
    }

    public void UpdateUser(
        Person Person,
        string? email,
        bool isActive,
        DateTime updatedAt)
    {
        Email = email ?? Email;
        IsActive = isActive;
        UpdatedAt = updatedAt;
    }
}