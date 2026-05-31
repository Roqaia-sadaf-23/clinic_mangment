using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.UpdateMedicalRecord
{
    public sealed class UpdateMedicalRecordAvaledator : AbstractValidator<UpdateDiagnosisMedicalRecordCommand>
    {
        public UpdateMedicalRecordAvaledator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.AppointmentId).NotEmpty().WithMessage("AppointmentId is required");
            RuleFor(x => x.Diagnosis).NotEmpty().WithMessage("Diagnosis is required");
            RuleFor(x => x.PrescriptionID).NotEmpty().WithMessage("PrescriptionID is required");
        }
    }
}
