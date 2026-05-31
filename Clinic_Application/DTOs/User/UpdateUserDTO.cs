namespace Clinic_Application.DTOs.User
{
 
        public class UpdateUserDTO
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public int? PhoneNumber { get; set; }
          //  public string Role { get; set; } = string.Empty;
       public bool IsActive { get; set; }
    }
}