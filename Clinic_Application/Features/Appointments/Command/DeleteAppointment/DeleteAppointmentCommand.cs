using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.DeleteAppointment
{
    public sealed record  DeleteAppointmentCommand(int Id) : IRequest<bool>;
   
}
