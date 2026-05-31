using Clinic_Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Users.Query.GetUser
{
    public sealed record GetUsersQuery() : IRequest<List<UserDTO>>;
    
}
