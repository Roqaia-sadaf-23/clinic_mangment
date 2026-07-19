using MediatR;
using System.IO;
using Clinic_Application.DTOs.Person;

namespace Clinic_Application.Features.people.Command.UpdateImage
{

    public sealed record UpdateImageCommand(
        int UserId,
        string ImagePath
    ) : IRequest<bool>;
}
