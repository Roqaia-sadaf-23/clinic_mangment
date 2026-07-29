using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed record CreateMedicalRecordCommand(int UserId,
    int AppointmentId,
    string Diagnosis,
    string? VisitDescription,
    string? Notes
) : IRequest<int>;

}
