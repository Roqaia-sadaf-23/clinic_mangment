using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.UpdatePerson
{
    public sealed class UpdatePersonValidator: AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonValidator()
        {

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name cannot be empty.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty.");
            RuleFor(x => x.NationalityNo).NotEmpty().WithMessage("Nationality number cannot be empty.");
        }
    }
}
