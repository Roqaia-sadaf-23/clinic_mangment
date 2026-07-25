using Clinic_Application.DTOs.Appintment;
using Clinic_Application.Features.Appointments.Command.CancelAppointment;
using Clinic_Application.Features.Appointments.Command.CompleteAppointment;
using Clinic_Application.Features.Appointments.Command.CreateApointment;
using Clinic_Application.Features.Appointments.Command.DeleteAppointment;
using Clinic_Application.Features.Appointments.Command.UpdateAppointment;
using Clinic_Application.Features.Appointments.Query.GetAppointmentById;
using Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdDoctors;
using Clinic_Application.Features.Appointments.Query.GetAvailableSlots;
using Clinic_Application.Features.Appointments.Query.GetDoctorAppointmentSummary;
using Clinic_Application.Features.Appointments.Query.GetPendingAppointment;
using Clinic_Application.Features.Appointments.Query.TodayDoctorAppointments;
using Clinic_Application.Features.Appointments.Query.GetAppointmentByUserIdPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Clinic_Application.Features.Appointments.Query.GetDoctorPatients;
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


        // =========================================================
        // Patient: create appointment
        // =========================================================

        [Authorize(Roles = "Patient,Admin")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAppointment(
            CreateAppointmentDTO request,
            CancellationToken cancellationToken)
        { 
            var command = new CreateAppointmentCommand(
                request.DoctorId,
                request.AppointmentDate,
                request.Notes);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Current user's appointments
        // مناسب للمريض، ويمكن للـHandler تحديد نوع المستخدم
        // =========================================================

        [Authorize(Roles = "Patient")]
        [HttpGet("patient/me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyAppointments(
            CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
            {
                return Unauthorized(new
                {
                    isSuccess = false,
                    message = "User is not authenticated."
                });
            }

            var result = await _mediator.Send(
                new GetAppointmentByUserIdPatientsQuery(userId),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Doctor home: appointment summary
        // =========================================================

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/me/summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorAppointmentSummary(
            CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
            new GetDoctorAppointmentSummaryQuery(userId),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Doctor home: today's appointments
        // =========================================================

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/me/today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodayDoctorAppointments(
            CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new GetTodayDoctorAppointmentsQuery(userId),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Doctor: all own appointments with optional filtering
        //// =========================================================

        [Authorize(Roles = "Doctor")]//
        [HttpGet("doctor/me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDoctorAppointments(
            [FromQuery] string? status,
            [FromQuery] DateTime? date,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            if (page <= 0)
                return BadRequest("Page must be greater than 0.");

            if (pageSize <= 0 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100.");

            var query = new GetAppointmentByUserIdDoctorsQuery(
                userId,
                status,
                date,
                page,
                pageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Doctor: own patients
        // =========================================================

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/me/patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetDoctorPatients(
            CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new GetDoctorPatientsQuery(userId),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Get appointment details
        // يجب التحقق داخل الـHandler أن المستخدم مرتبط بالموعد
        // =========================================================

        [Authorize(Roles = "Doctor,Patient,Admin")]
        [HttpGet("{id:int}", Name = "GetAppointmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppointmentById(
            int id,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Appointment id must be greater than 0.");

            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new GetAppointmentByIdQuery(id, userId),
                cancellationToken);

            return result is null ? NotFound() : Ok(result);
        }

        // =========================================================
        // Admin: all appointments
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllAppointments(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetAppointmentQuery(),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Admin: all pending appointments
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPendingAppointments(
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetPendingAppointmentQuery(),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Update appointment data
        // يمكن السماح للمريض قبل تأكيد الموعد فقط
        // =========================================================

        [Authorize(Roles = "Patient,Admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAppointment(
            int id,
            UpdateAppointmentDTO request,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Appointment id must be greater than 0.");

            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var command = new UpdateAppointmentCommand(
                id,
                userId,
                request.DoctorId,
                request.AppointmentDate,
                request.Notes);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Cancel appointment
        // الطبيب أو المريض يستطيع الإلغاء
        // =========================================================

        [Authorize(Roles = "Doctor,Patient")]
        [HttpPut("{id:int}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelAppointment(
            int id,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Appointment id must be greater than 0.");

            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new CancelAppointmentCommand(id, userId),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Complete appointment
        // الطبيب فقط
        // =========================================================

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id:int}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteAppointment(
            int id,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Appointment id must be greater than 0.");

            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new CompleteAppointmentCommand(userId ,id ),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Delete appointment
        // الحذف النهائي للأدمن فقط
        // =========================================================

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAppointment(
            int id,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Appointment id must be greater than 0.");

            var result = await _mediator.Send(
                new DeleteAppointmentCommand(id),
                cancellationToken);

            return result
                ? Ok(new
                {
                    isSuccess = true,
                    message = "Appointment deleted successfully."
                })
                : NotFound(new
                {
                    isSuccess = false,
                    message = "Appointment not found."
                });
        }

        // =========================================================
        // Patient: available slots
        // =========================================================

        [Authorize(Roles = "Patient")]
        [HttpGet("available-slots")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAvailableSlots(
            [FromQuery] int doctorId,
            [FromQuery] DateTime date,
            CancellationToken cancellationToken)
        {
            if (doctorId <= 0)
                return BadRequest("Doctor id must be greater than 0.");

            if (date.Date < DateTime.UtcNow.Date)
                return BadRequest("Appointment date cannot be in the past.");

            var result = await _mediator.Send(
                new GetAvailableSlotsQuery(doctorId, date),
                cancellationToken);

            return Ok(result);
        }

        // =========================================================
        // Helper
        // =========================================================

        private bool TryGetCurrentUserId(out int userId)
        {
            var userIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userIdValue, out userId);
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


        //    //********create appointment 
        //    [HttpPost("create")]
        //    public async Task<ActionResult<Result<AppointmentDTO>>> CreateAppointment(
        //[FromBody] CreateAppointmentCommand command,
        //CancellationToken cancellationToken)
        //    {
        //        try
        //        {
        //            var result = await _mediator.Send(command, cancellationToken);
        //            return Ok(result);
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            return BadRequest(new
        //            {
        //                isSuccess = false,
        //                message = ex.Message
        //            });
        //        }
        //        catch (UnauthorizedAccessException ex)
        //        {
        //            return Unauthorized(new
        //            {
        //                isSuccess = false,
        //                message = ex.Message
        //            });
        //        }
        //    }
        //    //GetCountPendingAppointmentByDoctorIdQuery



        //    [HttpGet("count-pending/{id}")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> GetCountPendingAppointmentByDoctorId(int id, CancellationToken cancellationToken)
        //    {



        //        var result = await _mediator.Send(new GetCountPendingAppointmentByDoctorIdQuery(id));

        //        return result <=0 ? NotFound() : Ok(result);
        //    }





        //    [HttpGet("{id}", Name = "GetAppointmentById")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> GetAppointmentById(int id, CancellationToken cancellationToken)
        //    {



        //        var result = await _mediator.Send(new GetAppointmentByIdQuery(id));

        //        return result is null ? NotFound() : Ok(result);
        //    }



        //    [Authorize]
        //    [HttpGet("GetAppointmentByUserId")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> GetAppointmentByUserId(
        //CancellationToken cancellationToken)
        //    {
        //        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        if (string.IsNullOrWhiteSpace(userIdValue))
        //            return Unauthorized(new
        //            {
        //                isSuccess = false,
        //                message = "User is not authenticated."
        //            });

        //        var userId = Convert.ToInt32(userIdValue);

        //        var result = await _mediator.Send(
        //            new GetAppointmentByUserIdQuery(userId),
        //            cancellationToken);

        //        return Ok(result);
        //    }


        //    [HttpGet("AllAppointments")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> GetAllAppointment()
        //    {

        //        var result = await _mediator.Send(new GetAppointmentQuery());

        //        return result is null ? NotFound() : Ok(result);
        //    }



        //    [HttpGet("AllPendingAppointments")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> GetPendingAppointment()
        //    {

        //        var result = await _mediator.Send(new GetPendingAppointmentQuery());

        //        return result is null ? NotFound() : Ok(result);
        //    }

        //    [HttpPut("{id:int}")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> UpdateAppountment(int id, UpdateAppointmentDTO request)
        //    {
        //        var command = new UpdateAppointmentCommand(id, request.DoctorId, request.PatientId, request.AppointmentDate, request.Notes);

        //        var result = await _mediator.Send(command);
        //        if (result is null)
        //        {
        //            return BadRequest(result);
        //        }
        //        return Ok(result);
        //    }




        //    //cancel appointment
        //    [HttpPut("{id}/cancel")]
        //    public async Task<IActionResult> Cancel(
        //int id,
        //CancellationToken cancellationToken)
        //    {
        //        var result = await _mediator.Send(
        //            new CancelAppointmentCommand(id),
        //            cancellationToken);

        //        return Ok(result);
        //    }


        //    [HttpPut("{id}/Complete")]
        //    public async Task<IActionResult> Complete(
        //      int id,
        //            CancellationToken cancellationToken)
        //    {
        //        var result = await _mediator.Send(
        //            new CompleteAppointmentCommand(id),
        //            cancellationToken);

        //        return Ok(result);
        //    }

        //    [HttpDelete()]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public async Task<IActionResult> DeleteAppointment(int Id)
        //    {

        //    var command = new DeleteAppointmentCommand(Id);

        //        var result = await _mediator.Send(command);
        //        if (!result)
        //        {
        //            return BadRequest(result);
        //        }
        //        return Ok(result);
        //    }




        //    [HttpGet("available-slots")]
        //    public async Task<IActionResult> GetAvailableSlots(
        //int doctorId,
        //DateTime date)
        //    {
        //        var result = await _mediator.Send(
        //            new GetAvailableSlotsQuery(doctorId, date));

        //        return Ok(result);
        //    }



    }
}