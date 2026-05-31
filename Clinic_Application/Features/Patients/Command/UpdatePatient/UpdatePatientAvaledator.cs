using Clinic_Application.Features.Patients.Command.UpdatePatient;
using FluentValidation;


namespace Clinic_Application.Features.Patient.Command.UpdatePatient
{
    public sealed class UpdatePatientAvaledator: AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientAvaledator()

        {             RuleFor(x => x.BloodType).NotEmpty().WithMessage("Blood type is required.");
            RuleFor( x => x.ParsonId).GreaterThan(0).WithMessage("Person ID must be greater than 0.");
        }

    }
}
