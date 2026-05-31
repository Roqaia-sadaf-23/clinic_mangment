using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Command.CreateApointment;
using Clinic_Domain.Entities.Appointments;
using Clinic_Domain.Entities.Appointments;


//namespace Simple_ClinicBL.Mappings.AppointmentMapping;

namespace Clinic_Application.Mappings.AppointmentMapping;


    public static class AppointmentMappingExtensions
    {
        public static AppointmentDTO ToDTO(this Appointment appointment)
        {
            return new AppointmentDTO
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.StatusText,
                LastStatusDate = appointment.LastStatusDate,
                MedicalRecordId = appointment.MedicalRecordId,
                Notes = appointment.Notes
            };
        }

    //public static Appointment ToEntity(this CreateAppointmentCommand dto)
    //{
    //    return  Appointment.Create(
    //        dto.DoctorId,
    //        dto.PatientId,

    //        dto.AppointmentDate,
    //       // dto.DurationInMinutes,
    //        dto.Notes
    //    );
    //}

        public static void UpdateEntity(this UpdateAppointmentDTO dto, Appointment appointment)
        {
            appointment.Update(
                dto.DoctorId,
                dto.PatientId,
                dto.AppointmentDate,
                dto.Notes
            );
        }
    }
