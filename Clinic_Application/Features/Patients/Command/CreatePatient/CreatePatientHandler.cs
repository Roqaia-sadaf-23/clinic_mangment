using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Patient;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PatientEntity = Clinic_Domain.Entities.Patient;

namespace Clinic_Application.Features.Patients.Command.CreatePatient
{

    public sealed class CreatePatientHandler :
        IRequestHandler<CreatePatientCommand, PatientDTO>
    {
        private readonly IAppDBContext _dbContext;
        public CreatePatientHandler(IAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PatientDTO> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var personExists = await _dbContext.People
    .AnyAsync(p => p.ID == request.PersonId, cancellationToken);

            if (!personExists)
                throw new Exception($"Person with Id {request.PersonId} not found");

            var patient = PatientEntity.Create(request.BloodType, request.PersonId);

          //  throw new Exception($"DEBUG: request.PersonId={request.PersonId}, patient.PersonId={patient.PersonId}");
           
            
            
            await _dbContext.Patients.AddAsync(patient, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new PatientDTO
            {
                Id = patient.Id,
                BloodType = patient.BloodType,
                PersonId = patient.PersonId
            };
        }
    }
}