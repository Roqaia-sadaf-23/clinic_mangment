using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.patient;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Queries.GetPatient
{
    public class GetPatientInfoQueryHandler(IAppDBContext context) : IRequestHandler<GetPatientInfoQuery, List<PatientInfoDto>>
    {
       

       public async Task<List<PatientInfoDto>> Handle(
    GetPatientInfoQuery request,
    CancellationToken cancellationToken)
        {
            var result = await context.Patients
                .Join(
                    context.People,
                    patient => patient.PersonId,
                    person => person.ID,
                    (patient, person) => new { patient, person }
                )
                .Select(x => new PatientInfoDto
                {
                    Id = x.patient.Id,

                    firstName = x.person.FirstName,
                    lastName = x.person.LastName,
                    PhoneNumber = x.person.PhoneNumber,
                    Note = x.person.Note,
                    BloodType=x.patient.BloodType,
                    Age=x.person.Age,
                  PersonId=x.patient.PersonId,
                    ImagePath = x.person.ImagePath,
                        UserId =context.Users.Where(u=> u.PersonId == x.patient.PersonId)
                        .Select(u => u.Id).FirstOrDefault()

                })
                .ToListAsync(cancellationToken);

            return result;
        }
            // TODO: implement querying logic using _context
           
    }
}



