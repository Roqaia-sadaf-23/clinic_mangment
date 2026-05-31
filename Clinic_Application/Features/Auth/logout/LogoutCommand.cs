using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Auth.logout
{
    public sealed record LogoutCommand(string Email, string RefreshToken) : IRequest<string>
    {
    }
}
