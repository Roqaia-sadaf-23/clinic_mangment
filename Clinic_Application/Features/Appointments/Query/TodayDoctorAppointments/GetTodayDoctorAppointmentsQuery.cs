using Clinic_Application.DTOs.Appintment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.TodayDoctorAppointments
{
    public record  class GetTodayDoctorAppointmentsQuery(int UserId) : IRequest<List<AppointmentInfoDTO>>  
    {
    }
}
