using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Simple_ClinicBL.DTOs.Appintment



namespace Clinic_Application.DTOs.Appintment
{
    public class CreateAppointmentDTO
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        //public int DurationInMinutes { get; set; }
        public string? Notes { get; set; }
    }
}