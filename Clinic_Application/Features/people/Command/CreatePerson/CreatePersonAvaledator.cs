using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.CreatePerson
{
    public class CreatePersonValidator:
       AbstractValidator<CreatePersonCommand>
    {

        public CreatePersonValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.NationalityNo).NotEmpty().WithMessage("Nationality number is required.");
        }   
    }
}
