using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Person;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Query.GetPeeple
{
    public sealed record GetPeopleQuery() : IRequest<List<PersonDTO>> 
    {
    }
}
