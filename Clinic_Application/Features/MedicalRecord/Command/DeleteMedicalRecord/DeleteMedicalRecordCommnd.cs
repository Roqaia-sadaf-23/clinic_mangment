using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.DeleteMedicalRecord
{
    public sealed record DeleteMedicalRecordCommand(int Id) : IRequest<bool>;
    
}
