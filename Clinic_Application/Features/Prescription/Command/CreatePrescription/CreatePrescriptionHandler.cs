using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using entity = Clinic_Domain.Entities.Prescription;  

namespace Clinic_Application.Features.Prescription.Command.CreatePrescription
{
    public sealed class CreatePrescriptionHandler(IAppDBContext context) : IRequestHandler<CreatePrescriptionCommand, int>
    {
        public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
        var prescription= entity.Create(request.MedicationName, request.Frequency, request.Dosage, request.SpecialInstructions);
                context.Prescriptions.Add(prescription);
                await context.SaveChangesAsync(cancellationToken);
            return prescription.Id; 
        }
    }
}
