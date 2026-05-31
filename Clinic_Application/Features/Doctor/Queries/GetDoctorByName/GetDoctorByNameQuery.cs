using Clinic_Application.DTOs.Doctor;
using Clinic_Application.Features.Doctor.Queries.GetDoctorByName;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Queries.GetDoctorByName
{
    public sealed record GetDoctorByNameQuery(string Name) : IRequest<DoctorInfoDto>;
   
}
