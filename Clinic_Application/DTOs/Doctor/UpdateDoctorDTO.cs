using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Doctor
{

    public class UpdateDoctorDTO
    {
       // public int Id { get; set; }
        public string Specialty { get; set; } = string.Empty;
       // public DateTime? HireDate { get; set; }
       
        public int PersonId { get; set; } 
        
        public int? ExperienceYears { get; set; }
    }
}