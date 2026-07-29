using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.patient
{
    public class DoctorPatientAppointmentDetailsDTO
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public string PatientName { get; set; } = string.Empty;
        public string? PatientImage { get; set; }

        public string? BloodType { get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime LastStatusDate { get; set; }

        public string? Note { get; set; }
       // public int? MedicalRecordId { get; set; }
    }
}

