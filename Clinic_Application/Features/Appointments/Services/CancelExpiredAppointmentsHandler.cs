
using Clinic_Domain.Entities.Appointments;
using global::Clinic_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace Clinic_Application.Features.Appointments.Services
{


    public class CancelExpiredAppointmentsHandler
        : IRequestHandler<CancelExpiredAppointmentsCommand, int>
    {
        private readonly IAppDBContext _context;

        public CancelExpiredAppointmentsHandler(IAppDBContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CancelExpiredAppointmentsCommand request,
            CancellationToken cancellationToken)
        {
            var appointments = await _context.Appointments
                .Where(a =>
                    a.AppointmentStatus == AppointmentStatus.Pending &&
                    a.AppointmentDate < DateTime.Now)
                .ToListAsync(cancellationToken);

            foreach (var appointment in appointments)
            {
                appointment.Cancel();
            }

            await _context.SaveChangesAsync(cancellationToken);

            return appointments.Count;
        }
    }
}
