
using Clinic_Application.DTOs.Patient;
using Clinic_Domain.Entities;
using Clinic_Domain.Entities;
//namespace Simple_ClinicBL.Mappings.PatientMapping;


namespace Clinic_Application.Mappings.PatientMapping;
    public static class PatientMappingExtensions
    {
        public static PatientDTO ToDTO(this Patient patient)
        {
            return new PatientDTO
            {
                Id = patient.Id,
                BloodType = patient.BloodType,
                PersonId = patient.PersonId
            };
        }

        public static Patient ToEntity(this CreatePatientDTO dto)
        {
            return  Patient.Create(
                dto.BloodType,
                dto.PersonId
            );
        }

        public static void UpdateEntity(this UpdatePatientDTO dto, Patient patient)
        {
            patient.Update(
                dto.BloodType,
                dto.PersonId
            );
        }
    }
