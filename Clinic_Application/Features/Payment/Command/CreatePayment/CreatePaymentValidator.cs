using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Payment.Command.CreatePayment
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>  
    {
        public CreatePaymentValidator() { 
        RuleFor(x => x.amount).NotEmpty().WithMessage("Amount is required.");
            //RuleFor(x => x.).GreaterThan(0).WithMessage("Amount must be greater than 0.");

        }
    }
}
