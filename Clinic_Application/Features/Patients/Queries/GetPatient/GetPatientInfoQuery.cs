using Clinic_Application.DTOs.Doctor;
using Clinic_Application.DTOs.patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Queries.GetPatient
{
    public sealed record  GetPatientInfoQuery : IRequest<List<PatientInfoDto>>; 
     
}
