using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.User
{
    public class UserDTO
    {
        public int Id { set; get; }       
        public int PersonId { set; get; }

        public string FirstName { set; get; } = string.Empty;
        public string LastName { set; get; } = string.Empty;
        public string  Email { set; get; }
        public string UserName { set; get; }
        public bool  IsActive { set; get; }
        public int? PhoneNumber { set; get; } 
        public String NationalityNo { set; get; }
        //public string PasswordHash { set; get; } = string.Empty;
        public string RoleName { set; get; } = string.Empty;
        public int? Age { set; get; }
        public string ?Address { set; get; }
        public byte?Gender { set; get; }
        public int NationalityCountryID { set; get; }
        public string? ImagePath { set; get; }
        public string? Note { set; get; }

    }


    
}
