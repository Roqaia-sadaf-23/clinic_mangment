using FluentValidation;


namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed class CreateMedicalRecordAvaledator: AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordAvaledator()
        {
            RuleFor(x => x.AppointmentId)
            .GreaterThan(0);

            RuleFor(x => x.Diagnosis)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.VisitDescription)
                .MaximumLength(1000);

            RuleFor(x => x.Notes)
                .MaximumLength(1000);
        }


    }
    }

