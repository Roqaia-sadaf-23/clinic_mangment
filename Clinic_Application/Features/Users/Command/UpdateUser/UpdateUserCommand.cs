using Clinic_Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Users.Command.UpdateUser
{
    public sealed record UpdateUserCommand (int id, string name, string email) : IRequest<UserDTO>
    {
    }
}
