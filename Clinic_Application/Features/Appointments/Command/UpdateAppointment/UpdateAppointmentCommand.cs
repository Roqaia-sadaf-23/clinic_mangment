using Clinic_Application.DTOs.Appintment;
using Clinic_Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.UpdateAppointment
{
    public sealed record class UpdateAppointmentCommand(int Id,int DoctorId,int PatientId,
        DateTime AppointmentDate,string? Notes) : IRequest<UpdateAppointmentDTO>
    {
    }
}
