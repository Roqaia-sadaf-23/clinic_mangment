using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Query.GetByIdMedicalRecord
{
    public sealed class GetByIdMedicalRecordQueryHandler(IAppDBContext context) : IRequestHandler<GetByIdMedicalRecordQuery, MedicalRecordDTO>
    {
        public async Task<MedicalRecordDTO> Handle(GetByIdMedicalRecordQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord = await context.MedicalRecords.Where(m => m.Id == request.id).FirstOrDefaultAsync(cancellationToken);
            if (medicalRecord == null)
            {
                throw new InvalidOperationException("Medical record not found");
            }

            return new MedicalRecordDTO
            {
                Id = medicalRecord.Id,
                Diagnosis = medicalRecord.Diagnosis,
                Notes = medicalRecord.Notes,
                VisitDescription = medicalRecord.VisitDescreption,
                AppointmentId = medicalRecord.AppointmentId,
                PaymentId = medicalRecord.PaymentId,
                PrescriptionId = medicalRecord.PrescriptionId
            };
        }
    }
}
