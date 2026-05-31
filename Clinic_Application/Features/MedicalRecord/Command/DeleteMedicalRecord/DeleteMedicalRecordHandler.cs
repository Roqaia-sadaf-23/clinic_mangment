using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.MedicalRecord.Command.DeleteMedicalRecord
{
    public sealed class DeleteMedicalRecordHandler(IAppDBContext context) : IRequestHandler<DeleteMedicalRecordCommand, bool>
    {
        public async Task<bool> Handle(DeleteMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord =   context.MedicalRecords.Where(p => p.Id == request.Id).FirstOrDefault();
            if (medicalRecord == null)
            {
                return await Task.FromResult(false);
            }
            context.MedicalRecords.Remove(medicalRecord);
           await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
    }
}
