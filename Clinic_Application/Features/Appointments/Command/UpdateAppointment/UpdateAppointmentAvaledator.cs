using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Command.CreateApointment;
using Clinic_Domain.Entities;
using FluentValidation;
    

namespace Clinic_Application.Features.Appointments.Command.UpdateAppointment
{
    public class UpdateAppointmentAvaledator :
        AbstractValidator<UpdateAppointmentCommand>
    {

        public UpdateAppointmentAvaledator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorID is required.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("PatientID is required.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("AppointmentDate is required.")
                .GreaterThan(DateTime.Now).WithMessage("AppointmentDate must be in the future.")
                .Must(date => date >= DateTime.Now && date <= DateTime.Now.AddYears(1))
                .WithMessage("AppointmentDate must be between now and the next year.");
        }
    }
}
