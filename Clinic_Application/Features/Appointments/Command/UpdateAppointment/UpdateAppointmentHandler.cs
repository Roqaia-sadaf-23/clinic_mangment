using Clinic_Application.Common.Interfaces;
using Clinic_Application.DTOs.Appintment;
using MediatR;
using MediatR;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Errors.Model;


namespace Clinic_Application.Features.Appointments.Command.UpdateAppointment
{
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentDTO>
    {
        private readonly ILogger<UpdateAppointmentHandler> _logger;
        private readonly IAppDBContext _context;

        public UpdateAppointmentHandler(IAppDBContext context, ILogger<UpdateAppointmentHandler> logger)
        {
            _context = context;
                        _logger = logger;
        }
        public async Task<UpdateAppointmentDTO> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
           var appointment = _context.Appointments.FirstOrDefault(a => a.Id == request.Id);
            if (appointment == null)
            {

                throw new NotFoundException();

            }
////request.PatientId,
//            appointment.Update(request.DoctorId,  request.AppointmentDate, request.Notes);
//            //   appointment.DoctorId = request.DoctorId;
//            //   appointment.PatientId = request.PatientId;
//            //   appointment.AppointmentDate = request.AppointmentDate;
//            ////   appointment.DurationInMinutes = request.DurationInMinutes;
//            //   appointment.Notes = request.Notes;
//            _context.Appointments.Update(appointment);
//            await _context.SaveChangesAsync(cancellationToken);

            var updateAppointmentDTO = new UpdateAppointmentDTO
            {
                DoctorId = appointment.DoctorId,
            //    PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes
            };

            return updateAppointmentDTO;
            //return Task.FromResult(Result<UpdateAppointmentDTO>.Success(updateAppointmentDTO)); 
        }
    }
}
