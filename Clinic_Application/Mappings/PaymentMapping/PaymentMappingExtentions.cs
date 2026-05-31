using Clinic_Application.DTOs.Payment;
using Clinic_Domain.Entities;
using Clinic_Domain.Entities;

namespace Clinic_Application.Mappings.PaymentMapping
{
    public static class PaymentMappingExtentions
    {
        public static PaymentDTO ToDTO(this Payment payment)
        {
            return new PaymentDTO
            {
                Id = payment.Id,
                PaymentDate = payment.CreatedAt,
                paymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
                Note = payment.Note
            };
        }

        public static Payment ToEntity(this CreatePaymentDTO dto)
        {
            return Payment.Create(dto.AppointmentId,dto.PatientId, dto.paymentMethod, 
                dto.Amount, dto.Note);


        }

        public static void UpdateEntity(this UpdatePaymentDTO dTO, Payment payment)
        {
            payment.UpdatePayment(dTO.amount, dTO.paymentMethod, dTO.Note);

        }
    }
}