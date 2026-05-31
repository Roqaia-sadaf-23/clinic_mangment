
using Clinic_Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
    using System.Security.Claims;


namespace Clinic_Flow.UserService
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(userId, out var id)
                    ? id
                    : null;
            }
        }
    }
}
