using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.patient;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Clinic_Application.Features.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetPatientByIdQuery, PatientInfoDto>
    {
      

           public async Task<PatientInfoDto> Handle(
    GetPatientByIdQuery request,
    CancellationToken cancellationToken)
        {
            var patient = await context.Patients
                .Where(p => p.Id == request.id)
                .Select(p => new PatientInfoDto

                { 
                    PatientId = p.Id,
                    PersonId = p.PersonId,
                    PatientName = p.Person.FirstName+" "+p.Person.LastName,
                    PhoneNumber = p.Person.PhoneNumber,
                    Note = p.Person.Note,
                    Age = p.Person.Age,
                    BloodType = p.BloodType,
                    PatientImage = p.Person.ImagePath,
                   
                    UserId = context.Users
                        .Where(u => u.PersonId == p.PersonId)
                        .Select(u => u.Id)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return patient!;
    

    }
    }
}
