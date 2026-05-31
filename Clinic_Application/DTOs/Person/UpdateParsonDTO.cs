using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Person
{
    public class UpdatePersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string NationalityNo { get; set; } = null!;

        public int? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }

        public byte? Gender { get; set; }

        public int NationalityCountryId { get; set; }
        //   public Country Country { get; set; }
        public string? ImagePath { get; set; }

        public string? Note { get; set; }
    }
}
