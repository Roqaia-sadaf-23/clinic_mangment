using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Appintment
{
    public class DoctorPatientDTO
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string? PatientImage { get; set; }

        public string? BloodType { get; set; }

        public int AppointmentsCount { get; set; }

        public DateTime LastAppointmentDate { get; set; }
    }
}
