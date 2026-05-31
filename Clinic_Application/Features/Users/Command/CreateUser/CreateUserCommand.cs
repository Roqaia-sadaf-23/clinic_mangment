using Clinic_Application.DTOs.User;
using MediatR;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    bool IsActive,
    string NationalityNo,

    int RoleId,

    int? PhoneNumber,
    int? Age,
    string? Address,
    byte? Gender,
    int NationalityCountryId,
    string? ImagePath,
    string? Note
) : IRequest<CreateUserDTO>;