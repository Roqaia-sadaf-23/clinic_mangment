using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;


namespace Clinic_Application.Features.Appointments.Query.TodayDoctorAppointments
{
    public sealed class GetTodayDoctorAppointmentsQueryHandler(IAppDBContext context) : IRequestHandler<GetTodayDoctorAppointmentsQuery, List<AppointmentDTO>>
    {
        public async Task<List<AppointmentDTO>> Handle(
    GetTodayDoctorAppointmentsQuery request,
    CancellationToken cancellationToken)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken);

            if (user == null)
                throw new Exception("User not found.");


            var doctor = await context.Doctors
                .FirstOrDefaultAsync(
                    d => d.PersonId == user.PersonId,
                    cancellationToken);

            if (doctor == null)
                throw new Exception("Doctor not found.");


            var todayAppointments = await context.Appointments
                .Where(a =>
                    a.DoctorId == doctor.Id &&
                    a.AppointmentDate.Date == DateTime.Today)
                .ToListAsync(cancellationToken);


            return todayAppointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                Status = a.AppointmentStatus.ToString(),
                LastStatusDate = a.LastStatusDate,
                MedicalRecordId = a.MedicalRecordId,
               // Notes = a.Notes
            }).ToList();
        }
    }
}
