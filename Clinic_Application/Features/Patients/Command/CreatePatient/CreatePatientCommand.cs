using Clinic_Application.DTOs.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Command.CreatePatient
{
    public sealed record CreatePatientCommand(string? BloodType, int PersonId) 
        : IRequest<PatientDTO>;
     
}
