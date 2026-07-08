using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Clinic_Application.Features.Role.Query
{
    public class GetRoleQueryHandler(IAppDBContext context) :
        IRequestHandler<GetRolesQuery, List<RoleDTO>>
    {
        public async Task<List<RoleDTO>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await context.Roles.AsNoTracking()
                .Select(r => new RoleDTO
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                    Description = r.Description
                })
                .ToListAsync(cancellationToken);
            return roles;
        }
    }
}
