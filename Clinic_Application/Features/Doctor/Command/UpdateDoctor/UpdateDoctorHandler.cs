using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic_Application.Common.Interfaces;
    using Clinic_Application.DTOs.Doctor;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using SendGrid.Helpers.Errors.Model;

namespace Clinic_Application.Features.Doctor.Command.UpdateDoctor
{
    

        public class UpdateDoctorHandler(IAppDBContext context)
            : IRequestHandler<UpdateDoctorCommand, UpdateDoctorDTO>
        {
            public async Task<UpdateDoctorDTO> Handle(
                UpdateDoctorCommand request,
                CancellationToken cancellationToken)
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(
                        u => u.Id == request.UserId,
                        cancellationToken);

                if (user is null)
                    throw new NotFoundException("User not found.");

                var doctor = await context.Doctors
                    .Include(d => d.Person)
                    .FirstOrDefaultAsync(
                        d => d.PersonId == user.PersonId,
                        cancellationToken);

                if (doctor is null)
                    throw new NotFoundException("Doctor not found.");

                doctor.UpdateDoctorInfo(
                    request.Specialization,
                    request.firstName,
                    request.lastName,
                    request.Age,
                    request.Note,
                    request.ExperienceYears);

                await context.SaveChangesAsync(cancellationToken);

                return new UpdateDoctorDTO
                {
                    firstName = doctor.Person.FirstName,
                    lastName = doctor.Person.LastName,
                    Age = doctor.Person.Age,
                    Note = doctor.Person.Note,
                    Specialization = doctor.Specialty,
                    experienceYears = doctor.ExperienceYears
                };
            }
        }
    
}
