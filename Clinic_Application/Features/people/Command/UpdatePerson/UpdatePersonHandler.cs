using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.UpdatePerson
{
    public class UpdatePersonHandler(IAppDBContext context) :
         IRequestHandler<UpdatePersonCommand, UpdatePersonDTO>
    {
        public async Task<UpdatePersonDTO> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
         var person = context.People.Find(request.Id) ;
            if (person == null)
            {
                return await Task.FromResult<UpdatePersonDTO>(null);
            }
      //  people.Update(person);
            person.Update(request.FirstName, request.LastName, request.NationalityNo,
                request.PhoneNumber, request.Age,
                request.Address,request.Gender,request.NationalityCountryId, request.ImagePath, request.Note);
            context.People.Update(person);
            context.SaveChangesAsync(cancellationToken);

            var updatepersondto = new UpdatePersonDTO
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalityNo = request.NationalityNo,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                Address = request.Address,
                Gender = request.Gender,
                NationalityCountryId = request.NationalityCountryId,
                ImagePath = request.ImagePath,
                Note = request.Note


            };
            return updatepersondto;
        }
    }
}
