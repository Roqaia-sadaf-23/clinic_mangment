using Clinic_Application.DTOs.Person;
using Clinic_Domain.Entities;


namespace Clinic_Application.Mappings.PersonMapping
{
    public static class PersonMappingExtensions
    {
        public static PersonDTO ToDTO(this Person person)
        {
            return new PersonDTO
            {
                Id = person.ID,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                NationalityNo = person.NationalityNo,
                Age = person.Age,
                Gender = person.Gender,
                Address = person.Address,
                NationalityCountryId = person.NationalityCountryId,
                ImagePath = person.ImagePath,
                Note = person.Note


            };
        }


        public static Person ToEntity(this CreatePersonDTO dto)
        {
            return Person.Create(
                dto.FirstName,
                dto.LastName,
                dto.NationalityNo,
                dto.PhoneNumber,
                dto.Age,
                dto.Address,
                dto.Gender,
                dto.NationalityCountryId,
                dto.ImagePath,
                                dto.Note);


        }



        public static void UpdateEntity(this UpdatePersonDTO dto, Person person)
        {
            person.Update(
                dto.FirstName,
                dto.LastName,
                dto.NationalityNo,
                dto.PhoneNumber,
                dto.Age,
                dto.Address,
                dto.Gender,
                dto.NationalityCountryId,
                dto.ImagePath,
                dto.Note);


        }


        }
}