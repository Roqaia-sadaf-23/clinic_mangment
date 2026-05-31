using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Command.CreatePrescription
{
    public sealed record CreatePrescriptionCommand(string MedicationName, string?
        Frequency, string? Dosage, string? SpecialInstructions) : IRequest<int>;
  
}
