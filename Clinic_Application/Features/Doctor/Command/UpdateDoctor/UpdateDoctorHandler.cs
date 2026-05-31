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

namespace Clinic_Application.Features.Doctor.Command.UpdateDoctor
{
    public class UpdateDoctorHandler(IAppDBContext context) : IRequestHandler<UpdateDoctorCommand, UpdateDoctorDTO>
    {
        public async Task<UpdateDoctorDTO> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {

            var doctor = await context.Doctors.FindAsync(
                 request.Id,
                cancellationToken);

            if (doctor == null)
            {
                throw new NotFoundException("Doctor not found");
            }

            doctor.Update(request.Specialty, 
                request.PersonId, request.ExperienceYears);
            context.Doctors.Update(doctor);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateDoctorDTO
            {
               // Id = doctor.Id,
                Specialty = doctor.Specialty,
               
                ExperienceYears = doctor.ExperienceYears,
                PersonId = doctor.PersonId
            };
        }
    }
}
