using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Appointments.Query.GetDoctorPatients
{
    public sealed class GetDoctorPatientsQueryHandler(IAppDBContext context):IRequestHandler<GetDoctorPatientsQuery,List<DoctorPatientDTO>>
    {
        public async Task<List<DoctorPatientDTO>> Handle(
    GetDoctorPatientsQuery request,
    CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.Id == request.UserId,
                    cancellationToken);

            if (user is null)
                throw new Exception("User not found.");

            var doctorId = await context.Doctors
                .AsNoTracking()
                .Where(d => d.PersonId == user.PersonId)
                .Select(d => (int?)d.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (doctorId is null)
                throw new Exception("Doctor not found.");

            var patients = await context.Appointments
                .AsNoTracking()
                .Where(a => a.DoctorId == doctorId.Value)
                .GroupBy(a => new
                {
                    a.PatientId,
                    a.Patient.Person.FirstName,
                    a.Patient.Person.LastName,
                    a.Patient.Person.ImagePath,
                    a.Patient.BloodType
                })
                .Select(group => new DoctorPatientDTO
                {
                    PatientId = group.Key.PatientId,
                    PatientName =
                        group.Key.FirstName + " " + group.Key.LastName,
                    PatientImage = group.Key.ImagePath,
                    BloodType = group.Key.BloodType,
                    AppointmentsCount = group.Count(),
                    LastAppointmentDate = group.Max(a => a.AppointmentDate)
                })
                .ToListAsync(cancellationToken);

            return patients;
        }


    }
}
