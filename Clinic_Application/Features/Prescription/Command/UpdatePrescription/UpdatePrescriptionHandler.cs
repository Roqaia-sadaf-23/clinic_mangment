using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Prescription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Command.UpdatePrescription
{
    public sealed class UpdatePrescriptionHandler(IAppDBContext context) : IRequestHandler<UpdatePrescriptionCommand, UpdatePrescriptionDTO>
    {
        public async Task<UpdatePrescriptionDTO> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
        {

            var prescription = await context.Prescriptions.FindAsync(request.Id, cancellationToken);
            if (prescription == null)
                return null;
            prescription.Update(request.MedicationName, request.Frequency, request.Dosage, request.SpecialInstructions);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdatePrescriptionDTO{
             
                MedicationName = prescription.MedicationName,
                Frequency = prescription.Frequency,
                Dosage = prescription.Dosage,
                SpecialInstructions = prescription.SpecialInstructions
            };
        }
    }
}
