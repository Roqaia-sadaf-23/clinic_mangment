using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.MedicalRecord;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Query.GetDoctorPatientMedicalRecord
{
    public sealed class GetDoctorPatientMedicalRecordsQueryHandler(IAppDBContext context) : IRequestHandler<GetDoctorPatientMedicalRecordsQuery, List<DoctorPatientMedicalRecordDTO>>
    {
        public async Task<List<DoctorPatientMedicalRecordDTO>> Handle(GetDoctorPatientMedicalRecordsQuery request, CancellationToken cancellationToken)
        {
            var personId = await context.Users
    .AsNoTracking()
    .Where(u => u.Id == request.UserId)
    .Select(u => (int?)u.PersonId)
    .FirstOrDefaultAsync(cancellationToken);

            if (personId is null)
                throw new Exception("User not found.");

            var doctorId = await context.Doctors
                .AsNoTracking()
                .Where(d => d.PersonId == personId.Value)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doctorId is null)
                throw new Exception("Doctor not found.");

            var patientBelongsToDoctor = await context.Appointments
                .AsNoTracking()
                .AnyAsync(
                    a => a.DoctorId == doctorId.Value &&
                         a.PatientId == request.PatientId,
                    cancellationToken);

            if (!patientBelongsToDoctor)
                throw new UnauthorizedAccessException(
                    "This patient does not belong to the current doctor.");

            var records = await context.MedicalRecords
                .AsNoTracking()
                .Where(m =>
                    m.Appointment.DoctorId == doctorId.Value &&
                    m.Appointment.PatientId == request.PatientId)
                .OrderByDescending(m => m.Appointment.AppointmentDate)
                .Select(m => new DoctorPatientMedicalRecordDTO
                {
                    MedicalRecordId = m.Id,
                    AppointmentId = m.AppointmentId,
                    
                    AppointmentDate = m.Appointment.AppointmentDate,
                    Diagnosis = m.Diagnosis,
                    VisitDescription = m.VisitDescreption,
                   
                    Notes = m.Notes,
                    PrescriptionId = m.PrescriptionId,
                    PaymentId = m.PaymentId
                })
                .ToListAsync(cancellationToken);

            return records;
        }
    }
}
