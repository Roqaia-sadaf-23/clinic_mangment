using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Appintment
{
    public class AppointmentInfoDTO
    {

        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        //   public int DurationInMinutes { get; set; } = 40;
        public string Status { get; set; }
        public DateTime LastStatusDate { get; set; }
        public int? MedicalRecordId { get; set; }
       // public string? Notes { get; set; }
    }
}
