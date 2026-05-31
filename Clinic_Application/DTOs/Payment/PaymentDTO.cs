using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.DTOs.Payment
{

    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? paymentMethod { get; set; }
        public decimal Amount  { get; set; }
        public string? Note { get; set; } 



    }
}