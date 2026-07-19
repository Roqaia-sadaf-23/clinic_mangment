using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Queries.GetCurrentDoctor
{
    namespace Clinic_Application.Features.Doctor.Queries.GetCurrentDoctor
    {
        public sealed class GetDoctorByUserIdHandler
            : IRequestHandler<GetDoctorByUserIdQuery, DoctorInfoDto?>
        {
            private readonly IAppDBContext _context;

            public GetDoctorByUserIdHandler(IAppDBContext context)
            {
                _context = context;
            }

            public async Task<DoctorInfoDto?> Handle(
                GetDoctorByUserIdQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Doctors
                    .AsNoTracking()
                    .Where(doctor =>
                        doctor.Person.User.Id == request.UserId)
                    .Select(doctor => new DoctorInfoDto
                    {
                        Id = doctor.Id,
                        PersonId = doctor.PersonId,
                        UserId = doctor.Person.User.Id,
                        firstName = doctor.Person.FirstName,
                        lastName = doctor.Person.LastName,
                        Age=doctor.Person.Age,
                        Specialization = doctor.Specialty,
                        experienceYears = doctor.ExperienceYears,
                        ImagePath = doctor.Person.ImagePath,
                        Note = doctor.Person.Note
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
