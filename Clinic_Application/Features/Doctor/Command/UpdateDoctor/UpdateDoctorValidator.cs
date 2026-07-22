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




            RuleFor(x => x.Specialization).NotEmpty().WithMessage("Specialty is required.");
            RuleFor(x => x.firstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.lastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be greater than 0.");
            RuleFor(x => x.ExperienceYears).GreaterThanOrEqualTo(0).WithMessage("Experience years must be greater than or equal to 0.");
                
        }
    }
}

