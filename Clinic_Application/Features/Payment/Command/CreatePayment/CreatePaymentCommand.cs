using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Command.CreatePayment
{
    public sealed record CreatePaymentCommand(int appointmentId,
        int patientId,
        string ?paymentMethod,
        decimal amount,
        string? note) : IRequest<int>;
     
}
