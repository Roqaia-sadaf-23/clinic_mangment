using Clinic_Application.Common.Interfaces;
using Clinic_Application.Features.Appointments.Command.DeleteAppointment;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteAppointmentHandler
    : IRequestHandler<DeleteAppointmentCommand, bool>
{
    private readonly IAppDBContext _dbContext;

    public DeleteAppointmentHandler(IAppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _dbContext.Appointments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (appointment == null)
            return false;

        _dbContext.Appointments.Remove(appointment);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}