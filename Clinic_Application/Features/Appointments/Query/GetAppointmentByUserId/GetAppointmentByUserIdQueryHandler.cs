using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserId
{
    public sealed class GetAppointmentByUserIdQueryHandler(IAppDBContext context,IMediator mediator)
        : IRequestHandler<GetAppointmentByUserIdQuery, List<AppointmentDTO>>
    {
        public async Task<List<AppointmentDTO>> Handle(
            GetAppointmentByUserIdQuery request,
            CancellationToken cancellationToken)
        {

            await mediator.Send(
     new CancelExpiredAppointmentsCommand(),
     cancellationToken);
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null) {


                throw new Exception($"User with ID {request.UserId} not found.");
            }
                
            var patientId = await context.Patients
                .Where(p => p.PersonId == user.PersonId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if(patientId == null)
            {
                throw new Exception($"Patient with Person ID {user.PersonId} not found.");
            }
            var doctorId = await context.Doctors
                .Where(d => d.PersonId == user.PersonId)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);


            if(doctorId == null)
            {
                throw new Exception($"Doctor with Person ID {user.PersonId} not found.");
            }
            var appointments = await context.Appointments
     .Where(a =>
         (patientId != null && a.PatientId == patientId.Value) ||
         (doctorId != null && a.DoctorId == doctorId.Value))
     .Select(a => new AppointmentDTO
     {
         Id = a.Id,
         DoctorId = a.DoctorId,
         PatientId = a.PatientId,
         AppointmentDate = a.AppointmentDate,
         Status = a.AppointmentStatus.ToString(),
         LastStatusDate = a.LastStatusDate,
         MedicalRecordId = a.MedicalRecordId,
         Notes = a.Notes
     })
     .ToListAsync(cancellationToken);

            return appointments;
        }
    }
}