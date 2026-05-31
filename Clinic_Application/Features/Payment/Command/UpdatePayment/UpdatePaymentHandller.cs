using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Payment;
using MediatR;
using entity = Clinic_Domain.Entities.Payment;

namespace Clinic_Application.Features.Payment.Command.UpdatePayment
{
    public sealed class UpdatePaymentHandller(IAppDBContext context) : IRequestHandler<UpdatePaymentCommand, UpdatePaymentDTO>
    {
        public async Task<UpdatePaymentDTO> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FindAsync(request.Id, cancellationToken);

            if (payment == null)
                return null;
            
                 payment.UpdatePayment(request.amount, request.paymentMethod, request.note);
                context.Payments.Update(payment);
                await context.SaveChangesAsync(cancellationToken);
            return new UpdatePaymentDTO
            {
                paymentMethod = payment.PaymentMethod,
                amount = payment.Amount     ,
                Note = payment.Note

            };

        }
    }
}