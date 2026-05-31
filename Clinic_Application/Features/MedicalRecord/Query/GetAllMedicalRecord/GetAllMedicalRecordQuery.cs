using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Query.GetAllMedicalRecord
{
    public sealed record GetAllMedicalRecordQuery : IRequest<List<MedicalRecordDTO>>;
    
}
