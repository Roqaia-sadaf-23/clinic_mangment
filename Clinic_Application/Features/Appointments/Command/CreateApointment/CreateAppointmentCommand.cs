
using Clinic_Application.DTOs.Appintment;
using Clinic_Domain.Common.Results;
using Clinic_Domain.Entities;
using MediatR;


namespace Clinic_Application.Features.Appointments.Command.CreateApointment
{
    public sealed record CreateAppointmentCommand(  int DoctorId,DateTime AppointmentDate,string? Notes) : IRequest<Result<AppointmentDTO>>
    {
         
    }

}
