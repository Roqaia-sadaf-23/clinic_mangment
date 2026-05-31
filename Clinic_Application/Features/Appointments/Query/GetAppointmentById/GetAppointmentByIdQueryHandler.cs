using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Mappings.AppointmentMapping;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentById
{
    internal class GetAppointmentByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetAppointmentByIdQuery, AppointmentDTO>
    {

        public async Task<AppointmentDTO> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await context.Appointments.FindAsync( request.Id , cancellationToken);
            if (appointment == null)
            {
                return null!;
            }

            return appointment.ToDTO();



        }
}
}
