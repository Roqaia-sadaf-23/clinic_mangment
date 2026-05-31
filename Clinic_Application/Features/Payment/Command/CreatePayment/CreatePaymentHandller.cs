using Clinic_Application.Common.Interfaces;
using MediatR;
using entity = Clinic_Domain.Entities.Payment;

namespace Clinic_Application.Features.Payment.Command.CreatePayment
{
    public sealed class CreatePaymentHandller(IAppDBContext context) : IRequestHandler<CreatePaymentCommand, int>
    {
        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
         var payment = entity.Create(request.appointmentId, request.patientId,request.paymentMethod, request.amount, request.note);
            context.Payments.Add(payment);
            await context.SaveChangesAsync(cancellationToken);
            return payment.Id;
        }
    }
}
