using Clinic_Application.Common.Interfaces;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entity = Clinic_Domain.Entities.MedicalRecord;
using Clinic_Application.Common.Exceptions;
namespace Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord
{
    public sealed class CreateMedicalRecordHandler(IAppDBContext context)
        : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        public async Task<int> Handle(
            CreateMedicalRecordCommand request,
            CancellationToken cancellationToken)
        {
            var appointment = await context.Appointments
                .FirstOrDefaultAsync(
                    appointment => appointment.Id == request.AppointmentId,
                    cancellationToken);

            if (appointment is null)
            {
             throw new ConflictException(

                    "Appointment was not found.");
            }

            var medicalRecordExists = await context.MedicalRecords
                .AnyAsync(
                    record =>
                        record.AppointmentId == request.AppointmentId,
                    cancellationToken);

            if (medicalRecordExists)
            {
                throw new ConflictException(
     "A medical record already exists for this appointment.");
            }

            if (appointment.AppointmentStatus != AppointmentStatus.Completed)
            {
                throw new ConflictException(
                    "A medical record can only be created for a completed appointment.");
            }



            //if (!string.Equals(
            //        appointment.AppointmentStatus.ToString(),
            //        "Completed",
            //        StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new InvalidOperationException(
            //        "A medical record can only be created for a completed appointment.");
            //}

            var medicalRecord = Entity.Create(
                request.Diagnosis.Trim(),
                string.IsNullOrWhiteSpace(request.Notes)
                    ? null
                    : request.Notes.Trim(),
                string.IsNullOrWhiteSpace(request.VisitDescription)
                    ? null
                    : request.VisitDescription.Trim(),
                request.AppointmentId);

            context.MedicalRecords.Add(medicalRecord);

            await context.SaveChangesAsync(cancellationToken);

            return medicalRecord.Id;
        }
    }
}