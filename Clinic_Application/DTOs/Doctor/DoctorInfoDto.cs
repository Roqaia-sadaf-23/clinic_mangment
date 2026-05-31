using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Doctor
{
    public class DoctorInfoDto
    {
      public int Id { get; set; }
       public int PersonId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int? Age { get; set; }
        public string? Note { get; set; }

        public string? Specialization { get; set; } = null;
        public string? ImagePath { get; set; }
        public int UserId { get; set; }



    }
}
