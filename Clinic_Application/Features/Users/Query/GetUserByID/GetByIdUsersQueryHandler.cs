using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.User;
using Clinic_Application.Mappings.UserMapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Users.Query.GetUserByID
{
    public sealed class GetByIdUsersQueryHandler(IAppDBContext context)
        : IRequestHandler<GetUserByIdQuery, UserDTO?>
    {
        public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Id == request.Id)
                .Select(u => u.ToDTO())
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}