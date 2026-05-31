using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Mappings.AppointmentMapping;
using Clinic_Domain.Common.Results;
//using Microsoft.Extensions.Caching.Hybrid;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
 



namespace Clinic_Application.Features.Appointments.Command.CreateApointment { 

public sealed class CreateAppointmentHandler(IAppDBContext context, ICurrentUserService currentUserr)
    : IRequestHandler<CreateAppointmentCommand, Result<AppointmentDTO>>
{

        public async Task<Result<AppointmentDTO>> Handle(
    CreateAppointmentCommand request,
    CancellationToken cancellationToken)
    {
            
            var userId = currentUserr.UserId.Value;

            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            var patientId = await context.Patients
                .Where(p => p.PersonId == user.PersonId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync(cancellationToken);

            // 1- لا يسمح بتاريخ في الماضي
            if (request.AppointmentDate < DateTime.Now)
                throw new ArgumentException("Appointment date cannot be in the past.");

            // 2- التحقق من دوام العيادة
            var clinicStart = request.AppointmentDate.Date.AddHours(12);
            var clinicEnd = request.AppointmentDate.Date.AddHours(22);

            var duration = TimeSpan.FromMinutes(40);

            if (request.AppointmentDate < clinicStart ||
                request.AppointmentDate.Add(duration) > clinicEnd)
            {
                throw new ArgumentException(
                      "Appointment must be between 12:00 PM and 10:00 PM.");
            }

            // 3- التحقق أن الموعد غير محجوز
            var isBooked = await context.Appointments.AnyAsync(
                a => a.DoctorId == request.DoctorId
                  && a.AppointmentDate == request.AppointmentDate
                  && a.AppointmentStatus != AppointmentStatus.Cancelled,
                cancellationToken);

            if (isBooked)
            {
                throw new ArgumentException(
                     "This appointment time is already booked.");
            }


            var resultAppointment = Appointment.Create(
            request.DoctorId,
          patientId,
            request.AppointmentDate,
          //  request.DurationInMinutes,
            request.Notes,
          userId
        );

        await context.Appointments.AddAsync(resultAppointment, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        var dto = resultAppointment.ToDTO();
        return dto;
    }
}}