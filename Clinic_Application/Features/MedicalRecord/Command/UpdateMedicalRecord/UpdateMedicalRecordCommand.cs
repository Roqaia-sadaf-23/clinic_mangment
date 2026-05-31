using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.UpdateMedicalRecord
{
    public sealed record UpdateDiagnosisMedicalRecordCommand(int Id, string Diagnosis, string? Notes, string? VisitDescreption, int AppointmentId, int? PaymentID, int? PrescriptionID) : IRequest<UpdateDiagnosisMedicalRecordDTO>;
}
