using Clinic_Application.Features.Doctor.Command.DeleteDoctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.DeleteDoctor
{
    public sealed record DeleteDoctorCommand(int Id) : IRequest<bool>;
    
}
