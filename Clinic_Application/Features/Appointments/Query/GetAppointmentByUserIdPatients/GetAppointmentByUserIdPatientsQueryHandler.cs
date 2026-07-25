using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdPatients
{
    public sealed class GetAppointmentByUserIdPatientsQueryHandler(IAppDBContext context,IMediator mediator)
        : IRequestHandler<GetAppointmentByUserIdPatientsQuery, List<PatientAppointmentDTO>>
    {
        public async Task<List<PatientAppointmentDTO>> Handle(
            GetAppointmentByUserIdPatientsQuery request,
            CancellationToken cancellationToken)
        {

     //       await mediator.Send(
     //new CancelExpiredAppointmentsCommand(),
     //cancellationToken);
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null) {


                throw new Exception($"User with ID {request.UserId} not found.");
            }
                
            var patientId = await context.Patients
                .Where(p => p.PersonId == user.PersonId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if(patientId == null)
            {
                throw new Exception($"Patient with Person ID {user.PersonId} not found.");
            }
         
            var appointments = await context.Appointments
     .Where(a =>
         (patientId != null && a.PatientId == patientId.Value)).ToListAsync(cancellationToken);
     
            return appointments.Select(a => new PatientAppointmentDTO
            {
                Id = a.Id,
                DoctorName = a.Doctor.Person.FirstName + " " + a.Doctor.Person.LastName,
                Specialty = a.Doctor.Specialty,
                AppointmentDate = a.AppointmentDate,
                Status = a.AppointmentStatus.ToString(),
                Notes = a.Doctor.Person.Note
            }).ToList() ;


          
        }
    }
}