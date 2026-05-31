using Clinic_Application.Common.Interfaces;
using Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord;
using MediatR;
using Entity = Clinic_Domain.Entities.MedicalRecord;

namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed class CreateMedicalRecordHandler(IAppDBContext context) : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        public async Task<int> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
        
            var medicalRecord =   Entity.Create(request.Diagnosis, request.Notes, request.VisitDescription, request.AppointmentId,request.PaymentID,request.PrescriptionId);
          


            context.MedicalRecords.Add(medicalRecord);
          await context.SaveChangesAsync(cancellationToken);

          await context.Appointments.FindAsync(new object[] { request.AppointmentId },
              cancellationToken).AsTask().ContinueWith(async appointmentTask =>
            {
                var appointment = appointmentTask.Result;
                if (appointment != null)
                {
                    appointment.AttachMedicalRecord(medicalRecord.Id);
                    context.Appointments.Update(appointment);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }, cancellationToken);

            return medicalRecord.Id;
        }
    }
}
