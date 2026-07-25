using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Common.Moduls;
using MediatR;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdDoctors
{
    public record  GetAppointmentByUserIdDoctorsQuery(
   int UserId,
       string? Status,
       DateTime? Date,
       int Page,
       int PageSize)
       : IRequest<PagedResultDTO<DoctorAppointmentDTO>>;
    }




