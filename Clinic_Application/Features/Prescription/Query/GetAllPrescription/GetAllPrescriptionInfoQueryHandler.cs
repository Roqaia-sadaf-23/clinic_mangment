using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Prescription;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Query.GetAllPrescription
{
    public sealed class GetAllPrescriptionInfoQueryHandler(IAppDBContext context) : IRequestHandler<GetAllPrescriptionInfoQuery, List<PrescriptionDTO>>
    {
        public async Task<List<PrescriptionDTO>> Handle(GetAllPrescriptionInfoQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = context.Prescriptions.Select(p => new PrescriptionDTO
            {
                Id = p.Id,
             
                MedicationName = p.MedicationName,
                Dosage = p.Dosage,
                Frequency = p.Frequency,
               SpecialInstructions = p.SpecialInstructions
            }).ToList();
            return await Task.FromResult(prescriptions);    
        }
    }
}
