using Clinic_Application.DTOs.Appintment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetDoctorPatients
{
    public record class GetDoctorPatientsQuery(int UserId):IRequest<List<DoctorPatientDTO>>
    {
    }
}
