using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.UpdateMedicalRecord
{
    public sealed class UpdateDiagnosisMedicalRecordHandler(IAppDBContext context) : IRequestHandler<UpdateDiagnosisMedicalRecordCommand, UpdateDiagnosisMedicalRecordDTO>
    {
        public async Task<UpdateDiagnosisMedicalRecordDTO> Handle(UpdateDiagnosisMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = await context.MedicalRecords.FindAsync( request.Id , cancellationToken);

            if(medicalRecord == null ) 
                return null;

          medicalRecord.UpdateDiagnosis(request.Diagnosis, request.Notes, request.VisitDescreption, request.AppointmentId, request.PaymentID, request.PrescriptionID);

            context.MedicalRecords.Update(medicalRecord);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdateDiagnosisMedicalRecordDTO
            {
                Diagnosis = medicalRecord.Diagnosis,
                Notes = medicalRecord.Notes,
                VisitDescription = medicalRecord.VisitDescreption  ,
                AppointmentId = medicalRecord.AppointmentId,
                PaymentId = medicalRecord.PaymentId,
                PrescriptionId = medicalRecord.PrescriptionId
            };
        }
    }
}
