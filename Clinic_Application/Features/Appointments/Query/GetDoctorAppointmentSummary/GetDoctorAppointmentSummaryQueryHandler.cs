using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Query.GetDoctorAppointmentSummary;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Application.Features.Appointments.Query.GetDoctorAppointmentSummary
{
    public sealed class GetDoctorAppointmentSummaryQueryHandler : IRequestHandler<GetDoctorAppointmentSummaryQuery, AppointmentSummaryDTO>
    {

        private readonly IAppDBContext _context;
        public GetDoctorAppointmentSummaryQueryHandler(IAppDBContext context)
        {
            _context = context;
        }
        public async Task<AppointmentSummaryDTO> Handle(
    GetDoctorAppointmentSummaryQuery request,
    CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken);

            if (user == null)
            {
                throw new Exception(
                    $"User with ID {request.UserId} not found.");
            }


            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(
                    d => d.PersonId == user.PersonId,
                    cancellationToken);

            if (doctor == null)
            {
                throw new Exception(
                    $"Doctor not found.");
            }


            var appointments = await _context.Appointments.AsNoTracking()
                .Where(a => a.DoctorId == doctor.Id)
                .ToListAsync(cancellationToken);


            return new AppointmentSummaryDTO
            {
                TodayAppointments = appointments.Count(a =>
                    a.AppointmentDate.Date == DateTime.Today),

                PendingAppointments = appointments.Count(a =>
                    a.AppointmentStatus == AppointmentStatus.Pending),

                CompletedAppointments = appointments.Count(a =>
                    a.AppointmentStatus == AppointmentStatus.Completed),

                CancelledAppointments = appointments.Count(a =>
                    a.AppointmentStatus == AppointmentStatus.Cancelled)
            };
        }





        //public async Task<AppointmentSummaryDTO> Handle(GetDoctorAppointmentSummaryQuery request, CancellationToken cancellationToken)
        //{
        //    var uesr = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
        //    if (uesr == null)
        //    {
        //        throw new Exception($"User with ID {request.UserId} not found.");
        //    }

        //    var doctorId = _context.Doctors.FirstOrDefault(d => d.PersonId == uesr.PersonId).Id;

        //    if(doctorId == 0)
        //    {

        //        return null;
        //      //  throw new Exception($"Doctor with Person ID {uesr.PersonId} not found.");
        //    }
        //    var appointmentsPendingCount = _context.Appointments.Where(s => s.AppointmentStatus == AppointmentStatus.Pending && s.DoctorId == doctorId && s.AppointmentDate >= DateTime.Now).Count();
        //    if(appointmentsPendingCount == 0)
        //    {
        //        return null;
        //     //   throw new Exception($"No pending appointments found for Doctor");
        //    }

        //  var  appointmentsCancelledCount = _context.Appointments.Where(s => s.AppointmentStatus == AppointmentStatus.Cancelled && s.DoctorId == doctorId).Count();

        //    if (appointmentsCancelledCount == 0)
        //    {
        //        return null;
        //       // throw new Exception($"No cancelled appointments found for Doctor");
        //    }
        //    var appointmentsCompletedCount = _context.Appointments.Where(s => s.AppointmentStatus == AppointmentStatus.Completed && s.DoctorId == doctorId).Count();

        //    if(appointmentsCompletedCount == 0)
        //    {
        //        return null;
        //      //  throw new Exception($"No completed appointments found for Doctor");
        //    }

        //    var appointmentsTodayCount = _context.Appointments.Where(s => s.AppointmentDate.Date == DateTime.Now.Date && s.DoctorId == doctorId).Count();
        //  if(appointmentsTodayCount == 0)
        //    {
        //        return null;
        //    }

        //    return new AppointmentSummaryDTO
        //    {
        //        TodayAppointments = appointmentsTodayCount,
        //        PendingAppointments = appointmentsPendingCount,
        //        CompletedAppointments = appointmentsCompletedCount,
        //        CancelledAppointments = appointmentsCancelledCount
        //    };
        //}
    }
}
