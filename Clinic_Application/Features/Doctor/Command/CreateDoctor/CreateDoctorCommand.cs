using Clinic_Application.DTOs.Doctor;
using Clinic_Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.CreateDoctor
{
    public sealed record CreateDoctorCommand(string Specialty,DateTime?Hiredate,int PersonId,int? ExperienceYear) : IRequest<DoctorDTO>;
    

}
