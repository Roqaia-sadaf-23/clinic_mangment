using Clinic_Application.DTOs.Person;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Query.GetPersonById
{
    public sealed record GetPersonByIdQuery(int Id) : IRequest<PersonDTO?>;
    
}
