using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.CreateDoctor
{
    public class CreateDoctorValidator:AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorValidator()
        {
            RuleFor(x => x.Specialty).NotEmpty().WithMessage("Specialty is required.");
            RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("PersonId must be greater than 0.");
            RuleFor(x => x.Hiredate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("HireDate cannot be in the future.");
        }
    }
}
