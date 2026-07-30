using Clinic_Application.DTOs.Prescription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Query.GetDoctorPatientPrescriptions
{
    public sealed record GetDoctorPatientPrescriptionsQuery(int UserId, int PatientId) : IRequest<List<DoctorPatientPrescriptionDTO>>
    {
    }
}
