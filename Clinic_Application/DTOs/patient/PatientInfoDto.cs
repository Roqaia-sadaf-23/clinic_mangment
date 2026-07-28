using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.patient
{
    public class PatientInfoDto
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public int? UserId { get; set; }
        public int PersonId { get; set; }
        public string? BloodType { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public string? PhoneNumber { get; set; }
                public int? Age { get; set; }
        public string? Note { get; set; } 
        public string? PatientImage { get; set; }
        public int? MedicalRecordId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime ? LastStatusDate { get; set; }


    }
}
