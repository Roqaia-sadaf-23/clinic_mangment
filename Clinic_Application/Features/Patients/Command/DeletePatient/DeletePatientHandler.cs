using Clinic_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Patients.Command.DeletePatient
{
    public class DeletePatientHandler(IAppDBContext dbContext) : IRequestHandler<DeletePatientCommand, bool>
    {
       

            public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await dbContext.Patients
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (patient == null)
                return false;

            dbContext.Patients.Remove(patient);

            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
    
}
