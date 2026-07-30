using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Payment
{
   
        public sealed class DoctorPatientPaymentDTO
        {
            public int PaymentId { get; set; }

            public int AppointmentId { get; set; }

            public DateTime AppointmentDate { get; set; }

            public decimal Amount { get; set; }

            public string PaymentMethod { get; set; } = string.Empty;

         //   public string Status { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; }

            public string? Note { get; set; }
        }
    }
 
