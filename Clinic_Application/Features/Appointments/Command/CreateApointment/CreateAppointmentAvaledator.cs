using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Command.CreateApointment
{
	public sealed class CreateAppointmentAvaledator:
		AbstractValidator<CreateAppointmentCommand>
	{


		public CreateAppointmentAvaledator() 
		{ 
		DoctorID.NotEmpty().WithMessage("DoctorID is required.");	

			PatientID.NotEmpty().WithMessage("PatientID is required.");

			AppointmentDate.NotEmpty().WithMessage("AppointmentDate is required.")
				.GreaterThan(DateTime.Now).WithMessage("AppointmentDate must be in the future.");


			RuleFor(x => x.AppointmentDate)
	.Must(date => date >= DateTime.Now && date <= DateTime.Now.AddYears(1))
	.WithMessage("AppointmentDate must be between now and the next year.");
			//AppointmentDate.LessThan(DateTime.Now.AddYears(1)).WithMessage("AppointmentDate must be within the next year.").;

		}	


	}
}
