using Clinic_Domain.Common;


namespace Clinic_Domain.Entities
{
    public class Login: AuditableEntity
    {

        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public Login() { }

    }
}
