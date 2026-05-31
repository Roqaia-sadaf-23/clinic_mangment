using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Query.GetAllMedicalRecord
{
    public sealed class GetAllMedicalRecordQueryHandler(IAppDBContext context) : IRequestHandler<GetAllMedicalRecordQuery, List<MedicalRecordDTO>>
    {
        public Task<List<MedicalRecordDTO>> Handle(GetAllMedicalRecordQuery request, CancellationToken cancellationToken)
        {
     var result = context.MedicalRecords.Select(m => new MedicalRecordDTO
     {
         Id = m.Id,
         Diagnosis = m.Diagnosis,
         Notes = m.Notes,
         VisitDescription = m.VisitDescreption,
         AppointmentId = m.AppointmentId,
         PaymentId = m.PaymentId,
         PrescriptionId = m.PrescriptionId
     }).ToList();
            return Task.FromResult(result);
        }
    }
}
