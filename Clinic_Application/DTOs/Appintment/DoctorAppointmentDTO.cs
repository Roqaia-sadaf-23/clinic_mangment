using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Appintment
{
    public class DoctorAppointmentDTO
    {

        public int Id { get; set; }

        public int PatientId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string? PatientImage { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime LastStatusDate { get; set; }

  

        public string? Notes { get; set; }


    }
}
