using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Command.CreatePatient
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(x => x.BloodType).NotEmpty().WithMessage("Blood type is required.");
            RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("Person ID must be greater than 0.");
        }
    }
}
