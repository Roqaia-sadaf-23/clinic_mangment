using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using Clinic_Application.Mappings.PersonMapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Query.GetPersonById
{
    public sealed class GetPersonByIdQueryHandler(IAppDBContext context) 
        : IRequestHandler<GetPersonByIdQuery, PersonDTO?>
    {
        public async Task<PersonDTO?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await context.People.Where(p => p.ID == request.Id).
                Select(p => p.ToDTO()).FirstOrDefaultAsync(cancellationToken);
           
          
           
            return person;
        }

    }
}
