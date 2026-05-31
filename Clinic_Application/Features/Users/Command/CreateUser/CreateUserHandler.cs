using BCrypt.Net;
using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.User;
using Clinic_Domain.Entities;
using MediatR;

namespace Clinic_Application.Features.Users.Command.CreateUser
{
    public class CreateUserHandler(IAppDBContext context)
        : IRequestHandler<CreateUserCommand, CreateUserDTO>
    {
        public async Task<CreateUserDTO> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
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

            context.People.Add(person);

            await context.SaveChangesAsync(cancellationToken);
            int Prsonid = person.ID;
            // Create User
            var user = User.CreateUser(
                Prsonid,
                request.Email,
                request.UserName,
                hashedPassword,
                true
               
            );

            user.PersonId = person.ID;

            context.Users.Add(user);

            await context.SaveChangesAsync(cancellationToken);

            // Create UserRole
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = request.RoleId
            };

            context.UserRoles.Add(userRole);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateUserDTO
            {
                Id = user.Id,
                PersonId = user.PersonId,
                FirstName = user.Person.FirstName,
                LastName = user.Person.LastName,
                Age = user.Person.Age,
                Address = user.Person.Address,
                PhoneNumber = user.Person.PhoneNumber,
                Note = user.Person.Note,
                Email = user.Email,
                UserName = user.UserName,
                IsActive = user.IsActive,
                RoleName = user.UserRoles.FirstOrDefault()?.Role.RoleName,

                NationalityNo = user.Person.NationalityNo,
                NationalityCountryID = user.Person.NationalityCountryId,
                ImagePath = user.Person.ImagePath



            };
        }
    }
}