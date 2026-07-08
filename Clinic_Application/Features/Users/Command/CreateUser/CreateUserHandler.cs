using BCrypt.Net;
using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.User;
using Clinic_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User = Clinic_Domain.Entities.User;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clinic_Application.Features.Users.Command.CreateUser
{
    public class CreateUserHandler(IAppDBContext context)
        : IRequestHandler<CreateUserCommand, CreateUserDTO>
    {
        public async Task<CreateUserDTO> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            {
                throw new Exception("Email already exists");
            }

            // Hash Password
            var hashedPassword =
                BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create Person
            var person = Person.Create(
                request.FirstName,
                request.LastName,
                request.NationalityNo,
                request.PhoneNumber,
                request.Age,
                request.Address,
                    request.Gender,
                request.NationalityCountryId,
                request.ImagePath,
                request.Note
            );

            // Create User
            var user = User.CreateUser(
                person,
                request.Email,
                request.UserName,
                hashedPassword,
                true
               
            );

        
            var role = await context.Roles
        .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

            if (role == null)
                throw new Exception("Role not found");

            var userRole = UserRole.Create(user, request.RoleId);

            //context.People.Add(person);
            context.Users.Add(user);
            context.UserRoles.Add(userRole);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateUserDTO
            {
                Id = user.Id,
                PersonId = user.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Address = person.Address,
                PhoneNumber = person.PhoneNumber,
                Note = person.Note,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive,
                RoleName = role.RoleName,
                Gender=person.Gender,
                NationalityNo = person.NationalityNo,
                NationalityCountryID = person.NationalityCountryId,
                ImagePath = person.ImagePath
            };
        }
    }
}