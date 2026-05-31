using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinic_Application.DTOs.Payment
{

    public class UpdatePaymentDTO
    {
 
        public string? paymentMethod { get; set; }
        public decimal amount { get; set; }
        public string? Note { get; set; }

        

    }
}