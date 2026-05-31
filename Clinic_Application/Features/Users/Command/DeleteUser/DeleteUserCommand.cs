using MediatR;


namespace Clinic_Application.Features.Users.Command.DeleteUser
{
    public sealed record DeleteUserCommand(int id) : IRequest<bool>;
}
