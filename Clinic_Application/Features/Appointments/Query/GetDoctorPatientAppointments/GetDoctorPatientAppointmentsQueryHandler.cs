
   using Clinic_Application.Common.Interfaces;
    using Clinic_Application.DTOs.Appintment;
using Clinic_Application.DTOs.patient;
using Clinic_Application.DTOs.Patient;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Appointments.Query.GetDoctorPatientAppointments
{
 
    public sealed class GetDoctorPatientAppointmentsQueryHandler(
        IAppDBContext context
    ) : IRequestHandler<
        GetDoctorPatientAppointmentsQuery,
        List<DoctorPatientAppointmentDetailsDTO>>
    {
        public async Task<List<DoctorPatientAppointmentDetailsDTO>> Handle(
            GetDoctorPatientAppointmentsQuery request,
            CancellationToken cancellationToken)
        {
            var personId = await context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.UserId)
                .Select(u => (int?)u.PersonId)
                .FirstOrDefaultAsync(cancellationToken);

            if (personId is null)
                throw new Exception("User not found.");

            var doctorId = await context.Doctors
                .AsNoTracking()
                .Where(d => d.PersonId == personId.Value)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doctorId is null)
                throw new Exception("Doctor not found.");

            var patientBelongsToDoctor = await context.Appointments
                .AsNoTracking()
                .AnyAsync(
                    a => a.DoctorId == doctorId.Value &&
                         a.PatientId == request.PatientId,
                    cancellationToken);

            if (!patientBelongsToDoctor)
                throw new UnauthorizedAccessException(
                    "This patient does not belong to the current doctor.");

            return await context.Appointments
                .AsNoTracking()
                .Where(a =>
                    a.DoctorId == doctorId.Value &&
                    a.PatientId == request.PatientId)
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new DoctorPatientAppointmentDetailsDTO
                {
                    AppointmentId = a.Id,
                    PatientId = a.PatientId,
                  
                    BloodType=a.Patient.BloodType,
                    Age=a.Patient.Person.Age,
                    PatientName =
                        a.Patient.Person.FirstName + " " +
                        a.Patient.Person.LastName,
                    PatientImage = a.Patient.Person.ImagePath,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.StatusText,
                    PhoneNumber=a.Patient.Person.PhoneNumber,
                    LastStatusDate = a.LastStatusDate,
                
                    Note = a.Patient.Person.Note
                })
                .ToListAsync(cancellationToken);
        }
    }
}
