using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.User 
{
    public class CreateUserDTO
    {
     
        public int Id {  get; set; }
        public int PersonId { get; set; }

        public string FirstName { set; get; } = string.Empty;
        public string LastName { set; get; } = string.Empty;
        public string Email { set; get; }
        public string UserName { set; get; }    
        public bool IsActive { set; get; }
      //  public string Password { get; set; } = string.Empty;
        public int? PhoneNumber { set; get; }   

        public int? Age { set; get; }    
        public String NationalityNo { set; get; }
        //public string PasswordHash { set; get; } = string.Empty;
        public string RoleName { set; get; } = string.Empty;
        public string? Address { set; get; }
        public string? Gender { set; get; }
        public int NationalityCountryID { set; get; }
        public string? ImagePath { set; get; }
         public string? Note { set; get; }

    }
}