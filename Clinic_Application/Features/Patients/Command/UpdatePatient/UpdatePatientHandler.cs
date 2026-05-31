using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Patient;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Clinic_Application.Features.Patients.Command.UpdatePatient
{
    public class UpdatePatientHandler(IAppDBContext dbContext)
        : IRequestHandler<UpdatePatientCommand, UpdatePatientDTO>
    {
        public async Task<UpdatePatientDTO> Handle(
            UpdatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var patient = await dbContext.Patients.FindAsync(
                 request.Id ,
                cancellationToken);

            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }

            patient.Update(request.BloodType, request.ParsonId);
            dbContext.Patients.Update(patient);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdatePatientDTO
            {
               // Id = patient.Id,
                PersonId = patient.PersonId,
                BloodType = patient.BloodType
            };
        }
    }
}