using Clinic_Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Query.GetAllPayment
{
    public sealed record GetPaymentInfoQuery : IRequest<List<PaymentDTO>>;
}
