using Clinic_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Application.Features.Users.Command.DeleteUser
{
    public sealed class DeleteUserHandler(IAppDBContext context) : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = context.Users.Where(x => x.Id == request.id).FirstOrDefault();
            if (user is null)
            {
                return await Task.FromResult(false);
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);   

        }
    }
}
