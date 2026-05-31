using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Application.DTOs.MedicalRecord
{

    public class CreateMedicalRecordDTO
    {
        public string Diagnosis { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public string? VisitDescription { get; set; }
        public int AppointmentId { get; set; }
        public int? PaymentId { get; set; }
        public int? PrescriptionId { get; set; }
    
    
    
    }
}