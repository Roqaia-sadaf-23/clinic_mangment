    using Clinic_Application.DTOs.Appintment;
using Clinic_Application.DTOs.patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Clinic_Application.Features.Appointments.Query.GetDoctorPatientAppointments
{


    public sealed record GetDoctorPatientAppointmentsQuery(
        int UserId,
        int PatientId
    ) : IRequest<List<DoctorPatientAppointmentDetailsDTO>>;
}
