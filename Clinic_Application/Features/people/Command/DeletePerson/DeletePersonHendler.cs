using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.people.Command.DeletePerson
{
    public class DeletePersonHandler(IAppDBContext context): IRequestHandler<DeletePersonCommand, bool>
    {
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = context.People.FirstOrDefault(p => p.ID == request.id);
            if (person == null)
            {
                return false;
            }
            context.People.Remove(person);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
