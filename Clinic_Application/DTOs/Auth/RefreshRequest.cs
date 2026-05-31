

namespace Clinic_Application.DTOs.Auth
{
    public class RefreshResponseDTO
    {    
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string? Error { get; set; }
    }
}
