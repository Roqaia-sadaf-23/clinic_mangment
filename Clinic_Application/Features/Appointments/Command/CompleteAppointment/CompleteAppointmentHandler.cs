using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.CompleteAppointment
{
    public sealed class CompleteAppointmentHandler(IAppDBContext context) : IRequestHandler<CompleteAppointmentCommand, bool>
    {
        public async Task<bool> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await context.Appointments
                .FindAsync(new object[] { request.AppointmentId }, cancellationToken);
            if (appointment == null)
            {
                throw new ArgumentException("Appointment not found.");
                //return false;
            }
            appointment.Complete();
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
