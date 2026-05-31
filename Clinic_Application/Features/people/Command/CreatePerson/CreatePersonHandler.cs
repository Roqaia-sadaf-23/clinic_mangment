using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using MediatR;
using parsonEntity = Clinic_Domain.Entities.Person;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.CreatePerson
{
    public class CreatePersonHandler(IAppDBContext context) : IRequestHandler<CreatePersonCommand, PersonDTO>
    {
        public async Task<PersonDTO> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = parsonEntity.Create(request.FirstName, request.LastName, request.NationalityNo, request.PhoneNumber,
                request.Age, request.Address,
                request.Gender, request.NationalityCountryId, request.ImagePath, request.Note);
            context.People.Add(person);
            await context.SaveChangesAsync(cancellationToken);


            var personDTO = new PersonDTO
            {
                Id = person.ID,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalityNo = person.NationalityNo,
                PhoneNumber = person.PhoneNumber,
                Age = person.Age,
                Address = person.Address,
                Gender = person.Gender,
                NationalityCountryId = person.NationalityCountryId,
                ImagePath = person.ImagePath,
                Note = person.Note

            };
            return personDTO;
        }
    }
}