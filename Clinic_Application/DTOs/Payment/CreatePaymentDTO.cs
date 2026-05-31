
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Payment {

public class CreatePaymentDTO
{
    public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string? paymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }


         
}
 }