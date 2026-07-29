using Clinic_Application.Common.Interfaces;
using Clinic_Application.Common.Moduls;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Common;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdDoctors
{
    public sealed class GetAppointmentByUserIdDoctorsQueryHandler(IAppDBContext context) : IRequestHandler<
            GetAppointmentByUserIdDoctorsQuery,
            PagedResultDTO<DoctorAppointmentDTO>>
    {
        public async Task<PagedResultDTO<DoctorAppointmentDTO>> Handle(
            GetAppointmentByUserIdDoctorsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken);

            if (user is null)
                throw new NotFoundException("User not found.");

            var doctorId = await context.Doctors
                .AsNoTracking()
                .Where(d => d.PersonId == user.PersonId)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doctorId is null)
                throw new NotFoundException("Doctor profile not found.");

            IQueryable<Appointment> appointmentsQuery =
                context.Appointments
                    .AsNoTracking()
                    .Where(a => a.DoctorId == doctorId.Value);

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                if (!Enum.TryParse<AppointmentStatus>(
                        request.Status,
                        ignoreCase: true,
                        out var appointmentStatus))
                {
                    throw new ArgumentException(
                        "Invalid appointment status. " +
                        "Allowed values: Pending, Completed, Cancelled.");
                }

                appointmentsQuery = appointmentsQuery.Where(
                    a => a.AppointmentStatus == appointmentStatus);
            }

            if (request.Date.HasValue)
            {
                var startDate = request.Date.Value.Date;
                var endDate = startDate.AddDays(1);

                appointmentsQuery = appointmentsQuery.Where(
                    a => a.AppointmentDate >= startDate &&
                         a.AppointmentDate < endDate);
            }

            var totalCount = await appointmentsQuery
                .CountAsync(cancellationToken);

            var appointments = await appointmentsQuery
                .OrderBy(a => a.AppointmentDate)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(a => new DoctorAppointmentDTO
                {
                    Id = a.Id,
                    PatientId = a.PatientId,

                    // عدّلي هذه العلاقات حسب أسماء Entities عندك.
                    PatientName =
                        a.Patient.Person.FirstName + " " +
                        a.Patient.Person.LastName,

                    PatientImage = a.Patient.Person.ImagePath,

                    AppointmentDate = a.AppointmentDate,
                    Status = a.AppointmentStatus.ToString(),
                    LastStatusDate = a.LastStatusDate,
        
                    Notes = a.Patient.Person.Note
                })
                .ToListAsync(cancellationToken);

            return new PagedResultDTO<DoctorAppointmentDTO>
            {
                Items = appointments,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
