using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Queries.GetDoctorByName
{
    public sealed class GetDoctorByNameQueryHandler(IAppDBContext context) : IRequestHandler<GetDoctorByNameQuery, DoctorInfoDto>
    {
        public Task<DoctorInfoDto> Handle(GetDoctorByNameQuery request, CancellationToken cancellationToken)
        {

            var result = context.People.Where(d => d.FirstName == request.Name
                   || d.LastName == request.Name).Join(
                    context.Doctors,
                    person => person.ID,
                    doctor => doctor.PersonId,
                    (person, doctor) => new DoctorInfoDto
                    {
                        firstName = person.FirstName,
                        lastName = person.LastName,
                      // fullName = person.FirstName + " " + person.LastName
                        Specialization = doctor.Specialty,
                        Age = person.Age,
                        Note = person.Note,
                        ImagePath = person.ImagePath,
                        UserId = context.Users.Where(u => u.PersonId == person.ID).
                        Select(u => u.Id).FirstOrDefault()
                    }).FirstOrDefault();

            return Task.FromResult(result);





        }
    }
}
