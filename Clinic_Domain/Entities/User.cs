using Clinic_Domain.Entities;

namespace Clinic_Domain.Entities;

public class User
{
    private User() { }

    private User(
        int personId,
        string email,
        string userName,
        string passwordHash,
        bool isActive)
    {
        PersonId = personId;
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; set; }

    public int PersonId { get; set; }
    public virtual Person Person { get; set; } = null!;

    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public static User CreateUser(
        int personId,
        string email,
        string userName,
        string passwordHash,
        bool isActive)
    {
        return new User(personId, email, userName, passwordHash, isActive);
    }

    public void UpdateUser(
        string? email,
        bool isActive,
        DateTime updatedAt)
    {
        Email = email ?? Email;
        IsActive = isActive;
        UpdatedAt = updatedAt;
    }
}