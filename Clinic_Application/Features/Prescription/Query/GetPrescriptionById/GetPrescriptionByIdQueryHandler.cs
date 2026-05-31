using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Prescription;
using MediatR;

namespace Clinic_Application.Features.Prescription.Query.GetPrescriptionById
{
    public sealed class GetPrescriptionByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetPrescriptionByIdQuery, PrescriptionDTO>
    {
        public async Task<PrescriptionDTO> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var prescription = await context.Prescriptions.FindAsync( request.id, cancellationToken);
            if (prescription is null)
                return null;
            return new PrescriptionDTO
            {
                Id = prescription.Id,
MedicationName = prescription.MedicationName,           
                Dosage = prescription.Dosage,
                Frequency = prescription.Frequency,
                SpecialInstructions = prescription.SpecialInstructions,
            };


        }
    }
}
