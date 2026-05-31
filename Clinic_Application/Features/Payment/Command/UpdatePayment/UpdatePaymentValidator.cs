using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Command.UpdatePayment
{
    public sealed class UpdatePaymentValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentValidator()
        {
        RuleFor(x => x.paymentMethod).NotEmpty().WithMessage("Payment method is required.");
            RuleFor(x => x.amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");

        }
    }
}
