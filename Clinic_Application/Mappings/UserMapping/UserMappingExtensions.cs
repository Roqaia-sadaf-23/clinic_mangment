using Clinic_Application.DTOs.Person;
using Clinic_Application.DTOs.User;
using Clinic_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Mappings.UserMapping
{
    public static class UserMappingExtensions
    {


    public static UserDTO ToDTO(this User User)
        {
            return new UserDTO
            {
                Id = User.Id,
                PersonId = User.PersonId,
                FirstName = User.Person.FirstName,
                LastName = User.Person.LastName,
                PhoneNumber = User.Person.PhoneNumber,
                NationalityNo = User.Person.NationalityNo,
                Age = User.Person.Age,
                Gender = User.Person.Gender,
                Address = User.Person.Address,
                NationalityCountryID = User.Person.NationalityCountryId,
                ImagePath = User.Person.ImagePath,
                Note = User.Person.Note


            };
        }
    }
}
