using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Prescription;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Prescription.Query.GetDoctorPatientPrescriptions
{
    public sealed class GetDoctorPatientPrescriptionsQueryHandler(IAppDBContext context) : IRequestHandler<GetDoctorPatientPrescriptionsQuery, List<DoctorPatientPrescriptionDTO>>
    {
        public async Task<List<DoctorPatientPrescriptionDTO>> Handle(GetDoctorPatientPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            var PersonId = await context.Users.AsNoTracking().Where(u => u.Id == request.UserId).
                Select(a=>(int?)a.PersonId).FirstOrDefaultAsync(cancellationToken);
            if(PersonId == null)
            {
                throw new ArgumentException("User not found");
            }
            var doctorId = await context.Doctors.AsNoTracking().Where(d => d.PersonId == PersonId.Value)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);
          if(doctorId == null)
            {

                throw new ArgumentException("Doctor not found");    
            }

            var patientBelongsToDoctor = await context.Appointments
              .AsNoTracking()
              .AnyAsync(
                  a => a.DoctorId == doctorId.Value &&
                       a.PatientId == request.PatientId,
                  cancellationToken);

            if (!patientBelongsToDoctor)
                throw new UnauthorizedAccessException(
                    "This patient does not belong to the current doctor.");


            var prescriptions = await context.Prescriptions.AsNoTracking()
                .Where(p => p.MedicalRecord.Appointment.DoctorId == doctorId.Value && p.MedicalRecord.Appointment.PatientId  == request.PatientId).OrderByDescending(p => p.MedicalRecord.Appointment.AppointmentDate)  
                .Select(p => new DoctorPatientPrescriptionDTO
                {
                    PrescriptionId = p.Id,
                    MedicalRecordId = p.MedicalRecordId,
                    AppointmentId = p.MedicalRecord.AppointmentId,
                    AppointmentDate = p.MedicalRecord.Appointment.AppointmentDate,

                    MedicationName = p.MedicationName,
                    Dosage = p.Dosage,
                    Frequency = p.Frequency,
               SpecialInstructions = p.SpecialInstructions,
                }).ToListAsync(cancellationToken);
                
            return prescriptions;
        }
    }
}
