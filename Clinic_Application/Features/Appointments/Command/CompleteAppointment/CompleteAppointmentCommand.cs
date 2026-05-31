using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.CompleteAppointment
{
    public sealed record CompleteAppointmentCommand(int AppointmentId) : IRequest<bool>;
}
