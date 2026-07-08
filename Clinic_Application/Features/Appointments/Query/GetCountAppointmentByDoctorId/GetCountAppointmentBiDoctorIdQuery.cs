using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetCountAppointmentByDoctorId
{
    public sealed record class GetCountPendingAppointmentByDoctorIdQuery(int DoctorId) : IRequest<int>;
 
}
