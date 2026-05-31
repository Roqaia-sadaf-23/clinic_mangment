

namespace Clinic_Application.DTOs.Auth

{
    public class TokenResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string? Message { get; set; }
    }
}
