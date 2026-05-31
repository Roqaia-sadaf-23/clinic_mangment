using Clinic_Application.DTOs.Prescription;
using MediatR;


namespace Clinic_Application.Features.Prescription.Command.UpdatePrescription
{
    public sealed record UpdatePrescriptionCommand(int Id,string MedicationName, string?
        Frequency, string? Dosage, string? SpecialInstructions):IRequest<UpdatePrescriptionDTO>
    {
    }
}
