using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Command.CancelAppointment;
using Clinic_Application.Features.Appointments.Command.CompleteAppointment;
using Clinic_Application.Features.Appointments.Command.CreateApointment;
using Clinic_Application.Features.Appointments.Command.DeleteAppointment;
using Clinic_Application.Features.Appointments.Command.UpdateAppointment;
using Clinic_Application.Features.Appointments.Query.GetAppointmentById;
using Clinic_Application.Features.Appointments.Query.GetAppointmentByUserId;
using Clinic_Application.Features.Appointments.Query.GetAvailableSlots;
using Clinic_Application.Features.Appointments.Query.GetPendingAppointment;
using Clinic_Domain.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic_Flow.Controllers.Appointments
{

   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //"2026-06-01T17:00:00",
        //[HttpPost("create")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Result<AppointmentDTO>>> CreateAppointment(
        //    [FromBody] CreateAppointmentCommand command,
        //    CancellationToken cancellationToken)
        //{
        //    try {
        //       
        //    var result = await _mediator.Send(command, cancellationToken);

        //       return Ok(result);
        //    }
        //    catch (ArgumentException ex)
        //    { 

        //  //  if (!result.IsSuccess)
        //        return BadRequest(new
        //        {
        //            isSuccess = false,
        //            Message = ex.Message,
        //        });


        //}
        //}


        //********create appointment 
        [HttpPost("create")]
        public async Task<ActionResult<Result<AppointmentDTO>>> CreateAppointment(
    [FromBody] CreateAppointmentCommand command,
    CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
        }


        [HttpGet("{id}", Name = "GetAppointmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentById(int id, CancellationToken cancellationToken)
        {



            var result = await _mediator.Send(new GetAppointmentByIdQuery(id));

            return result is null ? NotFound() : Ok(result);
        }
        [Authorize]
        [HttpGet("GetAppointmentByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentByUserId(
    CancellationToken cancellationToken)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdValue))
                return Unauthorized(new
                {
                    isSuccess = false,
                    message = "User is not authenticated."
                });

            var userId = Convert.ToInt32(userIdValue);

            var result = await _mediator.Send(
                new GetAppointmentByUserIdQuery(userId),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet("AllAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAppointment()
        {

            var result = await _mediator.Send(new GetAppointmentQuery());

            return result is null ? NotFound() : Ok(result);
        }



        [HttpGet("AllPendingAppointments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPendingAppointment()
        {

            var result = await _mediator.Send(new GetAppointmentQuery());

            return result is null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppountment(int id, UpdateAppointmentDTO request)
        {
            var command = new UpdateAppointmentCommand(id, request.DoctorId, request.PatientId, request.AppointmentDate, request.Notes);

            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
       
        
        
        
        //cancel appointment
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(
    int id,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CancelAppointmentCommand(id),
                cancellationToken);

            return Ok(result);
        }


        [HttpPut("{id}/Complete")]
        public async Task<IActionResult> Complete(
          int id,
                CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CompleteAppointmentCommand(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAppointment(int Id)
        {
        
        var command = new DeleteAppointmentCommand(Id);

            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("available-slots")]
        public async Task<IActionResult> GetAvailableSlots(
    int doctorId,
    DateTime date)
        {
            var result = await _mediator.Send(
                new GetAvailableSlotsQuery(doctorId, date));

            return Ok(result);
        }

    }
}