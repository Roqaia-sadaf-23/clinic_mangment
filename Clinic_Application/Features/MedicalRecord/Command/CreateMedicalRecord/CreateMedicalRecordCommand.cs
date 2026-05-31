using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed record CreateMedicalRecordCommand(string Diagnosis,
        string? Notes,
        string? VisitDescription,
        int AppointmentId, int? PaymentID, int? PrescriptionId) : IRequest<int>;
  
}
