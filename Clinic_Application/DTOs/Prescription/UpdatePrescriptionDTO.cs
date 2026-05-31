using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Clinic_Application.DTOs.Prescription
{
    public class UpdatePrescriptionDTO
    {
        public string MedicationName { get; set; } = string.Empty;
        public string? Frequency { get; set; }
        public string? Dosage { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}