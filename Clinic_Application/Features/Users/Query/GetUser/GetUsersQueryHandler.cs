using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Users.Query.GetUser
{
    public class GetUsersQueryHandler(IAppDBContext context)
        : IRequestHandler<GetUsersQuery, List<UserDTO>>
    {
        public async Task<List<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await context.Users
                .Join(
                    context.UserRoles,
                    user => user.Id,
                    userRole => userRole.UserId,
                    (user, userRole) => new { user, userRole }
                )
                .Join(
                    context.Roles,
                    x => x.userRole.RoleId,
                    role => role.Id,
                    (x, role) => new { x.user, role }
                )
                .Join(
                    context.People,
                    x => x.user.PersonId,
                    person => person.ID,
                    (x, person) => new UserDTO
                    {
                        Id = x.user.Id,
                        UserName = x.user.UserName,
                        Email = x.user.Email,
                        IsActive = x.user.IsActive,

                        RoleName = x.role.RoleName,

                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Age = person.Age,
                        Address = person.Address,
                        Gender = person.Gender,
                        PhoneNumber = person.PhoneNumber,
                        NationalityNo = person.NationalityNo,
                        NationalityCountryID = person.NationalityCountryId,
                        ImagePath = person.ImagePath,
                        Note = person.Note
                    }
                )
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}