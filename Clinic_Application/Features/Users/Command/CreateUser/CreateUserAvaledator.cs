using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Users.Command.CreateUser
{
    public sealed class CreateUserAvaledator :
         AbstractValidator<CreateUserCommand>   
    {

        public CreateUserAvaledator() 
        {

            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Last Name is required.");
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.RoleId).GreaterThan(0)
                .WithMessage("Role ID must be greater than 0.");
            RuleFor(x => x.NationalityNo).Empty()
                .WithMessage("Nationality Number must be greater than 0.");


        }
    }
}
