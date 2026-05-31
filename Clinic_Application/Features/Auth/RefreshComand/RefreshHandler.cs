using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Auth;
using Clinic_Domain.Common.Refreshtoken;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Auth.RefreshComand
{
    public class RefreshHandler : IRequestHandler<Refreshcommand, RefreshResponseDTO>
    {
        string massage= "";
        private readonly IConfiguration _configuration;
        private readonly IAppDBContext _context;

        public RefreshHandler(IAppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<RefreshResponseDTO> Handle(Refreshcommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user == null)
                return null;



            var roleName = await _context.UserRoles
       .Where(ur => ur.UserId == user.Id)
       .Select(ur => ur.Role.RoleName)
       .FirstOrDefaultAsync(cancellationToken);

            var refreshtokens = await _context.RefreshTokens
                .Where(t => t.UserId == user.Id)
                .ToListAsync(cancellationToken);

            var token = refreshtokens.FirstOrDefault(t =>
                BCrypt.Net.BCrypt.Verify(request.RefreshToken, t.Token));

            if (token == null)
            {
                return new RefreshResponseDTO
                {
                    IsSuccess = false,
                    Error = "Invalid refresh token"
                };
            }

            if (token.RevokedAt != null)
            {
                return new RefreshResponseDTO
                {
                    IsSuccess = false,
                    Error = "Refresh token is revoked"
                };
            }

            if (token.ExpiresAt <= DateTime.UtcNow|| token.ExpiresAt == null)
            {
                return new RefreshResponseDTO
                {
                    IsSuccess = false,
                    Error = "Refresh token expired"
                };
            }

            //bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, token.Token);
            //if (!refreshValid)

            //    return new RefreshResponseDTO
            //    {
            //        IsSuccess = false,
            //        Error = "Invalid refresh token"
            //    };

            

            // Issue NEW access token (same claims & signing settings as login)
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
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Rotation: replace refresh token
            var newRefreshToken = GenerateRefreshToken.Generate();

            token.Token = BCrypt.Net.BCrypt.HashPassword(newRefreshToken);
            token.ExpiresAt = DateTime.UtcNow.AddDays(7);
            token.RevokedAt = null;

            await _context.SaveChangesAsync(cancellationToken);


            return new RefreshResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                IsSuccess = true,
                Error = null
            };
        }
    }
}
