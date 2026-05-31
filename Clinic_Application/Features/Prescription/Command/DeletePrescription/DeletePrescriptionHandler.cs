using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Prescription.Command.DeletePrescription
{
    public sealed class DeletePrescriptionHandler(IAppDBContext context) : IRequestHandler<DeletePrescriptionCommand, bool>
    {
        public Task<bool> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
        {
              var prescription = context.Prescriptions.Find(request.id);
                
            if(prescription == null) 
                return Task.FromResult(false);

            context.Prescriptions.Remove(prescription);
            context.SaveChangesAsync(cancellationToken);
            return Task.FromResult(true);

                }
    }
}
