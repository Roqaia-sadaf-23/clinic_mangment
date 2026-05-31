using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using Clinic_Application.Mappings.PersonMapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Query.GetPeeple
{
    public sealed class GetPeopleQueryHandler(IAppDBContext context) : IRequestHandler<GetPeopleQuery, List<PersonDTO>>
    {
        public Task<List<PersonDTO>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
          var  people= context.People.Select(s=>s.ToDTO()).ToList();
          return Task.FromResult(people);   
        }
    }

}
