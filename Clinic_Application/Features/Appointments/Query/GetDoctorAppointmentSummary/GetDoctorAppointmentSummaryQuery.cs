using Clinic_Application.DTOs.Appintment;
using MediatR;


namespace Clinic_Application.Features.Appointments.Query.GetDoctorAppointmentSummary
{
    public sealed record class GetDoctorAppointmentSummaryQuery(int UserId):
        IRequest<AppointmentSummaryDTO>;
}
