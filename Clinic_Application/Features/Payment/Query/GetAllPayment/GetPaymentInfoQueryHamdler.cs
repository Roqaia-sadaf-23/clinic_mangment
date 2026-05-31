using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Payment;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Query.GetAllPayment
{
    public sealed class GetPaymentInfoQueryHandler(IAppDBContext context) : IRequestHandler<GetPaymentInfoQuery, List<PaymentDTO>>
    {
        public Task<List<PaymentDTO>> Handle(GetPaymentInfoQuery request, CancellationToken cancellationToken)
        {
           var payments = context.Payments.Select(p => new PaymentDTO
           {
               Id = p.Id,
               PaymentDate = p.CreatedAt,
               paymentMethod = p.PaymentMethod,
               Amount = p.Amount,
               Note = p.Note
           }).ToList();
            return Task.FromResult(payments);
        }
    }
}
