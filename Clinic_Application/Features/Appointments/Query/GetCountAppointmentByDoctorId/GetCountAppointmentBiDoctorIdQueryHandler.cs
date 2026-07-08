using Clinic_Application.Common.Interfaces;
using Clinic_Application.Features.Appointments.Services;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetCountAppointmentByDoctorId
{
    public sealed class GetCountPendingAppointmentBiDoctorIdQueryHandler(IAppDBContext context, IMediator mediator) : IRequestHandler<GetCountPendingAppointmentByDoctorIdQuery, int>
    {
        public async Task<int> Handle(GetCountPendingAppointmentByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            await mediator.Send(
     new CancelExpiredAppointmentsCommand(),
     cancellationToken);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var appointments = context.Appointments.Where(a => a.DoctorId == request.DoctorId ).FirstOrDefault();
            if(DateOnly.FromDateTime(appointments.AppointmentDate) != today)
            {
                return 0;
                Console.WriteLine("No pending appointments for today.");
            }
            var count = context.Appointments.Where(a => a.DoctorId == request.DoctorId && a.AppointmentStatus == AppointmentStatus.Pending
     ).Count();
            
            //var appointments = context.Appointments.Where(a =>
            //{
            //    bool v = a.AppointmentDate == today;
            //    return a.DoctorId == request.DoctorId && a.AppointmentStatus == AppointmentStatus.Pending && v;
            //});
            //var count = appointments.Count();


            if (count == 0)
            {
                return 0;
            }
            return count;
        }
    }
}
