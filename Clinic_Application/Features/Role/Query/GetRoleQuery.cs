using Clinic_Application.DTOs.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Role.Query
{
    public sealed record class GetRolesQuery : IRequest<List<RoleDTO>>;
   
}
