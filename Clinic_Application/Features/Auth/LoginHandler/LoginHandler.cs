using BCrypt.Net;
    using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Auth;
using Clinic_Application.Features.Auth.LoginCommand;
using Clinic_Domain.Common.Refreshtoken;
using Clinic_Domain.Entities;
using MediatR;
    using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Clinic_Application.Features.Auth.LoginHandler
{
    public class loginHandler
        : IRequestHandler<loginCommand, TokenResponseDTO>
    {
        private readonly IConfiguration _configuration;
        private readonly IAppDBContext _context;
            public loginHandler(IAppDBContext context, IConfiguration configuration)
        {
            this._configuration = configuration;
            _context = context;
        }
        public async Task<TokenResponseDTO> Handle(loginCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.UserName == request.login ||
                    u.Email == request.login,
                    cancellationToken);
            if (user == null) 
                return null;
            var roleName =  await _context.UserRoles
           .Where(ur => ur.UserId == user.Id)
           .Select(ur => ur.Role.RoleName)
           .FirstOrDefaultAsync(cancellationToken);
            // Check for existing refresh token for the user
            var retoken = await _context.RefreshTokens
                .Where(rt => (rt.UserId == user.Id  || rt.ExpiresAt > DateTime.UtcNow))
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                return null;

            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!validPassword)
                return null;






            // Generate JWT token

            var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

    // username
    new Claim(ClaimTypes.Name, user.UserName),

    // email
    new Claim(ClaimTypes.Email, user.Email),

    // role
    new Claim(ClaimTypes.Role, roleName ?? "")
};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                ));
             
            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken.Generate();
            if (retoken == null)
            {
                retoken = new RefreshToken
                {
                    UserId = user.Id,
                    Token = BCrypt.Net.BCrypt.HashPassword(refreshToken),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    RevokedAt = null
                };

                await _context.RefreshTokens.AddAsync(retoken, cancellationToken);
            }
            else
            {
                retoken.Token = BCrypt.Net.BCrypt.HashPassword(refreshToken);
                retoken.ExpiresAt = DateTime.UtcNow.AddDays(7);
                retoken.RevokedAt = null;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new TokenResponseDTO
            {
                IsSuccess = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Message = null
            };
        }
    }
}



//var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

//// Create refresh token (random)
//var refreshToken = GenerateRefreshToken();

//// Store refresh token securely (hash + expiry + not revoked)
//student.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
//student.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
//student.RefreshTokenRevokedAt = null;

//return Ok(new TokenResponse
//{
//    AccessToken = accessToken,
//    RefreshToken = refreshToken
//});