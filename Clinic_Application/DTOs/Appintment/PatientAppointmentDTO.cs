using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Appintment
{
    public class PatientAppointmentDTO
    {
        public int Id { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}
