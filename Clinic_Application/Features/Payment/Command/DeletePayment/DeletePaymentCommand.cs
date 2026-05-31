using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Command.DeletePayment
{
    public sealed record DeletePaymentCommand(int Id) : IRequest<bool>;
     
}
