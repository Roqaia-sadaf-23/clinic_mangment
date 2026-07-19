using Clinic_Application.DTOs.Doctor;
using MediatR;


namespace Clinic_Application.Features.Doctor.Queries.GetCurrentDoctor
{
    public record class GetDoctorByUserIdQuery(int UserId) : IRequest<DoctorInfoDto?>
    {
    }
}
