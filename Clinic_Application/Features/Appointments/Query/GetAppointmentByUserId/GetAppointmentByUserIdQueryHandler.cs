using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserId
{
    public sealed class GetAppointmentByUserIdQueryHandler(IAppDBContext context)
        : IRequestHandler<GetAppointmentByUserIdQuery, List<AppointmentDTO>>
    {
        public async Task<List<AppointmentDTO>> Handle(
            GetAppointmentByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                return new List<AppointmentDTO>();

            var patientId = await context.Patients
                .Where(p => p.PersonId == user.PersonId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var doctorId = await context.Doctors
                .Where(d => d.PersonId == user.PersonId)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

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