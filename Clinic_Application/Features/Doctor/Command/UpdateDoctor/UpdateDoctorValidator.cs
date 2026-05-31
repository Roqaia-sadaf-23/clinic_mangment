using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.UpdateDoctor
{
    public sealed class UpdateDoctorValidator : AbstractValidator<UpdateDoctorCommand
        >
    {
        public UpdateDoctorValidator() {




            RuleFor(x => x.Specialty).NotEmpty().WithMessage("Specialty is required.");
            RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("PersonId must be greater than 0.");
        }
    }
}

