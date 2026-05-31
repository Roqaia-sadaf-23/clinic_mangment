using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorEntity= Clinic_Domain.Entities.Doctor;

namespace Clinic_Application.Features.Doctor.Command.CreateDoctor
{
    public class CreateDoctorHandller(IAppDBContext context) : IRequestHandler<CreateDoctorCommand, DoctorDTO>
    {
        public async Task<DoctorDTO> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var personExists = await context.People
            .AnyAsync(p => p.ID == request.PersonId, cancellationToken);


            if (!personExists)
                throw new Exception($"Person with Id {request.PersonId} not found");



            var doctor = DoctorEntity.Create(request.Specialty, request.Hiredate, request.PersonId, request.ExperienceYear);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync(cancellationToken);
            return  await Task.FromResult(new DoctorDTO
            {
                Id = doctor.Id,
                Specialty = doctor.Specialty,
                HireDate = doctor.HireDate,
                PersonId = doctor.PersonId
            });
        }
    }
}
