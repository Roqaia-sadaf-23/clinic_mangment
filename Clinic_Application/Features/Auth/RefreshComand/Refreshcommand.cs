using Clinic_Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Auth.RefreshComand
{
    public sealed record Refreshcommand(string RefreshToken,string Email) : IRequest<RefreshResponseDTO>
    {
    }
}
