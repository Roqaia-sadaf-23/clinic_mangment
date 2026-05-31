using Clinic_Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.UpdateDoctor
{
    public sealed record UpdateDoctorCommand(
        int Id,
        string Specialty,
        int PersonId,
       
        int? ExperienceYears
        ) : IRequest<UpdateDoctorDTO>;
}