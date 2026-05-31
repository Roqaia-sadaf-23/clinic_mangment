using Clinic_Application.Common.Interfaces;
using MediatR;



namespace Clinic_Application.Features.Appointments.Command.CancelAppointment
{
    public class CancelAppointmentHandler(IAppDBContext context)
      : IRequestHandler<CancelAppointmentCommand, bool>
    {
         

      

        public async Task<bool> Handle(
            CancelAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointment = await context.Appointments
                .FindAsync(new object[] { request.AppointmentId }, cancellationToken);

            if (appointment == null)
                throw new ArgumentException("Appointment not found.");

            appointment.Cancel();

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }



}
