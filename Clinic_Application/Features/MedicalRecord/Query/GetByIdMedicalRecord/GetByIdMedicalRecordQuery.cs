using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Query.GetByIdMedicalRecord
{
    public sealed record GetByIdMedicalRecordQuery(int id) : IRequest<MedicalRecordDTO>;
  
}
