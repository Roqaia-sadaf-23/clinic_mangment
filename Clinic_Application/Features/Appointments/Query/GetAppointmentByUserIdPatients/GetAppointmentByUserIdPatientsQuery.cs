using Clinic_Application.DTOs.Appintment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdPatients
{
    public sealed record GetAppointmentByUserIdPatientsQuery(int UserId) : IRequest<List<PatientAppointmentDTO>>;
    
}
