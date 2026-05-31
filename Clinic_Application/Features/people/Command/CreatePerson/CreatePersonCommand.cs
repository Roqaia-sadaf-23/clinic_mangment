using Clinic_Application.DTOs.Person;
using Clinic_Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.CreatePerson
{
    public sealed record CreatePersonCommand(string FirstName, string LastName, string NationalityNo, 
        int? PhoneNumber, int? Age, string? Address, byte? Gender,
        int NationalityCountryId, string? ImagePath, string? Note) : IRequest<PersonDTO>;
    
}
