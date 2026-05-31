using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.CancelAppointment
{
    public sealed record CancelAppointmentCommand(int AppointmentId)
    : IRequest<bool>;
}
