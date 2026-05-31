using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Command.CreatePrescription
{
    public sealed class CreatePrescriptionAvaledator : AbstractValidator<CreatePrescriptionCommand>
    {
        public CreatePrescriptionAvaledator()
        {
            RuleFor(x => x.MedicationName)
                .NotEmpty().WithMessage("Medication name is required.")
                .MaximumLength(100).WithMessage("Medication name cannot exceed 100 characters.");
            RuleFor(x => x.Frequency)
                .MaximumLength(50).WithMessage("Frequency cannot exceed 50 characters.");
            RuleFor(x => x.Dosage)
                .MaximumLength(50).WithMessage("Dosage cannot exceed 50 characters.");
            RuleFor(x => x.SpecialInstructions)
                .MaximumLength(200).WithMessage("Special instructions cannot exceed 200 characters.");
        }
    }
}
