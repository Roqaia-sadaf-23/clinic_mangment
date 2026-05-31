using Clinic_Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Queries.GetDoctor_
{
    public sealed record GetDoctorInfoQuery : IRequest<List<DoctorInfoDto>>; 
    
}
