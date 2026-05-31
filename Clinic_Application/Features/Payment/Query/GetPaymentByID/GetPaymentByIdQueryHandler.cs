using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Payment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Query.GetPaymentByID
{
    public sealed class GetPaymentByIdQueryHandler(IAppDBContext context) : IRequestHandler<GetPaymentByIdQuery, PaymentDTO>
    {
        public async Task<PaymentDTO> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
        var payment = await context.Payments.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        if (payment == null)
            throw new InvalidOperationException("Payment not found");

        return new PaymentDTO
        {
            Id = payment.Id,
            PaymentDate = payment.CreatedAt,
            paymentMethod = payment.PaymentMethod,
            Amount = payment.Amount,
            Note = payment.Note
        };
        }
    }
}
