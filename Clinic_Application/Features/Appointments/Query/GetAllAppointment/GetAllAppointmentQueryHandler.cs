using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Services;
using Clinic_Application.Mappings.AppointmentMapping;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Appointments.Query.GetPendingAppointment
{
    public sealed class GetAppointmentQueryHandler(IAppDBContext context, IMediator mediator) : IRequestHandler<GetAppointmentQuery, List<AppointmentDTO>>
    {
        public async Task<List<AppointmentDTO>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            await mediator.Send(
      new CancelExpiredAppointmentsCommand(),
      cancellationToken);
            return await context.Appointments.Select(s=>s.ToDTO()).ToListAsync(cancellationToken);


        }
    }
}
