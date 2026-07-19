using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Auth;
using Clinic_Application.Features.Auth.LoginCommand;
using Clinic_Domain.Common.Refreshtoken;
using Clinic_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Clinic_Application.Features.Auth.LoginHandler
{
    public class LoginHandler
        : IRequestHandler<loginCommand, TokenResponseDTO>
    {
        private readonly IConfiguration _configuration;
        private readonly IAppDBContext _context;

        public LoginHandler(
            IAppDBContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TokenResponseDTO> Handle(
            loginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.UserName == request.login ||
                         u.Email == request.login,
                    cancellationToken);

            if (user is null)
            {
                return new TokenResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid username, email, or password."
                };
            }

            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!validPassword)
            {
                return new TokenResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid username, email, or password."
                };
            }

            var roleName = await _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.RoleName)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(roleName))
            {
                return new TokenResponseDTO
                {
                    IsSuccess = false,
                    Message = "User role was not found."
                };
            }




            var claims = new List<Claim>
   {
        new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString()),
         new Claim(ClaimTypes.Name,  user.UserName ?? string.Empty),
     
        new Claim(ClaimTypes.Role, roleName)
    }; 
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                claims.Add(
                    new Claim(ClaimTypes.Email, user.Email));
            }

            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException(
                    "JWT key is not configured.");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            var accessToken =
                new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken =
                GenerateRefreshToken.Generate();

            var storedRefreshToken =
                await _context.RefreshTokens
                    .FirstOrDefaultAsync(
                        rt => rt.UserId == user.Id,
                        cancellationToken);

            var hashedRefreshToken =
                BCrypt.Net.BCrypt.HashPassword(refreshToken);

            if (storedRefreshToken is null)
            {
                storedRefreshToken = new RefreshToken
                {
                    UserId = user.Id,
                    Token = hashedRefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    RevokedAt = null,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.RefreshTokens.AddAsync(
                    storedRefreshToken,
                    cancellationToken);
            }
            else
            {
                storedRefreshToken.Token = hashedRefreshToken;
                storedRefreshToken.ExpiresAt =
                    DateTime.UtcNow.AddDays(7);

                storedRefreshToken.RevokedAt = null;
                storedRefreshToken.CreatedAt =
                    DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new TokenResponseDTO
            {
                IsSuccess = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RoleName= roleName,
                Message = "Login successful."
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