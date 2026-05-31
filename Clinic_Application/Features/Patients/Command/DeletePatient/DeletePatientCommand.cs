using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Command.DeletePatient
{
    public sealed record DeletePatientCommand(int Id) : IRequest<bool>;
    
}
