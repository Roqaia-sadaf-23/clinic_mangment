using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Command.DeletePayment
{
    public sealed class DeletePaymentHandler(IAppDBContext context) : IRequestHandler<DeletePaymentCommand, bool>
    {
        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FindAsync(request.Id , cancellationToken);
            if (payment == null)
                return false;

            context.Payments.Remove(payment);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
