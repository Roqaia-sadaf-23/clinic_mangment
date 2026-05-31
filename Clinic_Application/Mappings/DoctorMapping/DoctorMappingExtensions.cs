using Clinic_Application.Common;
using Clinic_Application.DTOs.Doctor;
using Clinic_Domain.Entities;

namespace Clinic_Application.Mappings.DoctorMapping
{

    public static class DoctorMappingExtensions
    {
        public static DoctorDTO ToDTO(this Doctor doctor)
        {
            return new DoctorDTO
            {
                Id = doctor.Id,
                Specialty = doctor.Specialty,
                HireDate = doctor.HireDate,
                PersonId = doctor.PersonId
            };
        }

        public static Doctor ToEntity(this CreateDoctorDTO dto)
        {
            return  Doctor.Create(
                dto.Specialty,
                dto.HireDate,
                dto.PersonId,
                dto.ExperienceYears
            );
        }

        public static void UpdateEntity(this UpdateDoctorDTO dto, Doctor doctor)
        {
            doctor.Update(
                dto.Specialty,
           
                dto.PersonId,
                dto.ExperienceYears
            );
        }
    }
}