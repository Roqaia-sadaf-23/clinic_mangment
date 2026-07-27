using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Mappings.AppointmentMapping;
using Clinic_Domain.Entities.Appointments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetAppointmentById
{
    internal class GetAppointmentByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetAppointmentByIdQuery, AppointmentInfoDTO>
    {

        public async Task<AppointmentInfoDTO> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await context.Appointments
    .AsNoTracking()
    .Include(a => a.Doctor)
        .ThenInclude(d => d.Person)
    .Include(a => a.Patient)
        .ThenInclude(p => p.Person)
    .FirstOrDefaultAsync(
        a => a.Id == request.Id,
        cancellationToken);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            return appointment.Todoinfo();

    //        var appointment = await _context.Appointments
    //.AsNoTracking()
    //.Where(a => a.Id == request.Id)
    //.Select(a => new AppointmentInfoDTO
    //{
    //    Id = a.Id,

    //    DoctorName =
    //        a.Doctor.Person.FirstName + " " +
    //        a.Doctor.Person.LastName,

    //    PatientName =
    //        a.Patient.Person.FirstName + " " +
    //        a.Patient.Person.LastName,

    //    AppointmentDate = a.AppointmentDate,
    //    Status = a.AppointmentStatus.ToString(),
    //    LastStatusDate = a.LastStatusDate,
    //    MedicalRecordId = a.MedicalRecordId,
    //    Notes = a.Notes
    //})
    //.FirstOrDefaultAsync(cancellationToken);

    //        if (appointment == null)
    //        {
    //            throw new KeyNotFoundException("Appointment not found.");
    //        }

    //        return appointment;

        }
}
}
