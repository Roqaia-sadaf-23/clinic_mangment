using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.patient
{
    public class PatientInfoDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int PersonId { get; set; }
        public string? BloodType { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int? PhoneNumber { get; set; }
                public int? Age { get; set; }
        public string? Note { get; set; } 
        public string? ImagePath { get; set; }

   

    }
}
