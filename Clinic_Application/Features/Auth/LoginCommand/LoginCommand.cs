using Clinic_Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Auth.LoginCommand
{
    public sealed record loginCommand(
     string login,
     string Password
 ) : IRequest<TokenResponseDTO>;

}