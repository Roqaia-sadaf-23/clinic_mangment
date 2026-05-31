using FluentValidation;


namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed class CreateMedicalRecordAvaledator: AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordAvaledator()
        {
            RuleFor(x => x.Diagnosis).NotEmpty().WithMessage("Diagnosis is required.");
            RuleFor(x => x.AppointmentId).GreaterThan(0).WithMessage("Appointment ID must be greater than 0.");
            RuleFor(x => x.PaymentID).GreaterThan(0).WithMessage("Payment ID must be greater than 0.");
            RuleFor(x => x.PrescriptionId).GreaterThan(0).WithMessage("Prescription ID must be greater than 0.");
        }
    }
}
