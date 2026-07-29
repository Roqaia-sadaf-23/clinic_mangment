using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.MedicalRecord
{
    public sealed class DoctorPatientMedicalRecordDTO
    {
        public int MedicalRecordId { get; set; }
        public int AppointmentId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Diagnosis { get; set; } = string.Empty;
        public string? VisitDescription { get; set; }
        public string? Notes { get; set; }

        public int? PrescriptionId { get; set; }
        public int? PaymentId { get; set; }
    }
}
