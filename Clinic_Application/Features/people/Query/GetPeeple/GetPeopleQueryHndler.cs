using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using Clinic_Application.Mappings.PersonMapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.people.Query.GetPeeple
{
    public sealed class GetPeopleQueryHandler(IAppDBContext context) : IRequestHandler<GetPeopleQuery, List<PersonDTO>>
    {
        public Task<List<PersonDTO>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
          var  people= context.People.AsNoTracking().Select(s=>s.ToDTO()).ToList();
          return Task.FromResult(people);   
        }
    }

}
