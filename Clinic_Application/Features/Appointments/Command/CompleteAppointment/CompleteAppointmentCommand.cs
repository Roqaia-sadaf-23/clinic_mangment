using Clinic_Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.CompleteAppointment
{
    public sealed record CompleteAppointmentCommand(int userId, int AppointmentId) : IRequest<bool>;
}
