using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Doctor;
using Clinic_Domain.Entities;
using MediatR;


namespace Clinic_Application.Features.Doctor.Queries.GetDoctor_
{
    public class GetDoctorInfoQueryHandller(IAppDBContext context) : IRequestHandler<GetDoctorInfoQuery, List<DoctorInfoDto>>
    {
  

        public Task<List<DoctorInfoDto>> Handle(GetDoctorInfoQuery request, CancellationToken cancellationToken)
        {

            var result =context.Doctors.Join(
                context.People,
                doctor => doctor.PersonId,
                Person => Person.ID,
                (doctor, Person)  =>new DoctorInfoDto   
                {
                    Id = doctor.Id,
                    PersonId = Person.ID,
                    firstName = Person.FirstName,
                    lastName = Person.LastName,
                    Specialization = doctor.Specialty,
                    Age= Person.Age,
                        Note = Person.Note,
                        ImagePath = Person.ImagePath,
                    UserId = context.Users.Where(u => u.PersonId == Person.ID).
                    Select(u => u.Id).FirstOrDefault()
                }).ToList();
                return Task.FromResult(result);
        }
    }
}
