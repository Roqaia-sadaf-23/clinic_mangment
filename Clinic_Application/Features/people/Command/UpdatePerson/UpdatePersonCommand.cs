using Clinic_Application.DTOs.Person;
using MediatR;


namespace Clinic_Application.Features.people.Command.UpdatePerson
{
    public sealed record UpdatePersonCommand(int Id,string FirstName, string LastName, string NationalityNo,
        int? PhoneNumber, int? Age, string? Address, byte? Gender,
        int NationalityCountryId, string? ImagePath, string? Note) :
        IRequest<UpdatePersonDTO>;
}
