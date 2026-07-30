using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Prescription
{
    public sealed class DoctorPatientPrescriptionDTO
    {
        public int PrescriptionId { get; set; }

        public int MedicalRecordId { get; set; }

        public int AppointmentId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string MedicationName { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public string? SpecialInstructions { get; set; }
    }
}
