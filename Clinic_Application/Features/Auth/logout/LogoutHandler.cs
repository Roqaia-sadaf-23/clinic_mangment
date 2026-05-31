using Clinic_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Auth.logout
{
    public class LogoutHandler(IAppDBContext context) : IRequestHandler<LogoutCommand, string>
    {

        public async Task<string> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
                return "Ok";

            var refreshTokens = await context.RefreshTokens
                .Where(t => t.UserId == user.Id)
                .ToListAsync(cancellationToken);

            var token = refreshTokens.FirstOrDefault(t =>
                BCrypt.Net.BCrypt.Verify(request.RefreshToken, t.Token));

            if (token == null)
                return "Ok";

            token.RevokedAt = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            return "Logged out successfully";
        }

    }
}

