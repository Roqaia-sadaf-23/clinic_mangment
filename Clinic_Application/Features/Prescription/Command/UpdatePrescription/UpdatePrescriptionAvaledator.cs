using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Command.UpdatePrescription
{
    public sealed class UpdatePrescriptionAvaledator :
         AbstractValidator<UpdatePrescriptionCommand>
    {

        public UpdatePrescriptionAvaledator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
     
        }
    }
}
