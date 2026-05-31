using Clinic_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Doctor.Command.DeleteDoctor
{
    public class DeleteDoctorHandler(IAppDBContext dbContext) : IRequestHandler<DeleteDoctorCommand, bool>
    {
        public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {

            var Doctor = dbContext.Doctors.Where(p => p.Id == request.Id).FirstOrDefault();

            if (Doctor == null)
            {
                return await Task.FromResult(false);
            }

            dbContext.Doctors.Remove(Doctor);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);

        }
    }
}
