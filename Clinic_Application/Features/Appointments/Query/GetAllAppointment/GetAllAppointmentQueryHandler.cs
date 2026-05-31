using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Clinic_Application.Mappings.AppointmentMapping;
using Clinic_Domain.Entities.Appointments;

namespace Clinic_Application.Features.Appointments.Query.GetPendingAppointment
{
    public sealed class GetAppointmentQueryHandler(IAppDBContext context) : IRequestHandler<GetAppointmentQuery, List<AppointmentDTO>>
    {
        public async Task<List<AppointmentDTO>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {

            return await context.Appointments.Select(s=>s.ToDTO()).ToListAsync(cancellationToken);


        }
    }
}
