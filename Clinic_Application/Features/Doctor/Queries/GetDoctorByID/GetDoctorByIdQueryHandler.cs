using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Queries.GetDoctorByID
{
    public class GetDoctorByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetDoctorByIdQuery, DoctorInfoDto>
    {

           public async Task<DoctorInfoDto?> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Doctors
                .Where(d => d.Id == request.Id)
                .Join(
                    context.People,
                    doctor => doctor.PersonId,
                    person => person.ID,
                    (doctor, person) => new DoctorInfoDto
                    {
                        Id = doctor.Id,
                        PersonId = doctor.PersonId,
                        firstName = person.FirstName,
                        lastName = person.LastName,
                        Specialization = doctor.Specialty,
                        Age = person.Age,
                        Note = person.Note,
                        ImagePath = person.ImagePath,
                        UserId = context.Users.
                        Where(u => u.PersonId == person.ID).
                        Select(u=>u.Id).FirstOrDefault(),

                    })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }



            //var result = context.Doctors.Join(
            //    context.People,
            //    doctor => doctor.PersonId,
            //    person => person.ID,
            //    (doctor, person) => new DoctorInfoDto
            //    {
            //        firstName = person.FirstName,
            //        lastName = person.LastName,
            //        Specialization = doctor.Specialty,
            //        Age = person.Age,
            //        Note = person.Note,
            //        ImagePath = person.ImagePath
            //    }).FirstOrDefault(d => d.Id == request.Id);

            //return Task.FromResult(result);
        }
    }
