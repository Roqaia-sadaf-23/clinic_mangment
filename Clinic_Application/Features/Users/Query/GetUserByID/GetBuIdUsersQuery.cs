using Clinic_Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Users.Query.GetUserByID
{
    public sealed record GetUserByIdQuery(int Id) : IRequest<UserDTO>;
}
