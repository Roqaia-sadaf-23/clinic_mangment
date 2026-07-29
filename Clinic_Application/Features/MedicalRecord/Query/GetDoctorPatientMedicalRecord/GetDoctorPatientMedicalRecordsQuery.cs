using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clinic_Application.DTOs.MedicalRecord;

namespace Clinic_Application.Features.MedicalRecord.Query.GetDoctorPatientMedicalRecord
{

    public sealed record GetDoctorPatientMedicalRecordsQuery(
    int UserId,
    int PatientId
) : IRequest<List<DoctorPatientMedicalRecordDTO>>;
}
