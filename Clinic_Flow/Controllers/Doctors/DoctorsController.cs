using Clinic_Application.DTOs.Doctor;
using Clinic_Application.Features.Doctor.Command.CreateDoctor;
using Clinic_Application.Features.Doctor.Command.DeleteDoctor;
using Clinic_Application.Features.Doctor.Command.UpdateDoctor;
using Clinic_Application.Features.Doctor.Queries.GetCurrentDoctor;
using Clinic_Application.Features.Doctor.Queries.GetDoctor_;
using Clinic_Application.Features.Doctor.Queries.GetDoctorByID;
using Clinic_Application.Features.Doctor.Queries.GetDoctorByName;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Domain.Entities;
using Clinic_Flow.Controllers.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic_Flow.Controllers.Doctors
{
  // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
     
    public class DoctorsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IMediator mediator, ILogger<DoctorsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        // GET api/doctors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDoctorsInfo()
        {
            // Logic to get doctors
            var query = _mediator.Send(new GetDoctorInfoQuery());
            return query is null? NotFound(): Ok(query);
        }

        // GET api/doctors/{id}
        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorById(
            int id,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("Invalid doctor ID.");

            var currentUserIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(currentUserIdValue, out var currentUserId))
                return Unauthorized();

            var doctor = await _mediator.Send(
                new GetDoctorByIdQuery(id),
                cancellationToken
            );

            if (doctor is null)
                return NotFound("Doctor was not found.");

            var ownsProfile = doctor.UserId == currentUserId;
            var isAdmin = User.IsInRole("Admin");

            if (!ownsProfile && !isAdmin)
                return Forbid();

            return Ok(doctor);
        }

        // GET api/doctors/by-name/{name}

        [HttpGet("by-name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorByNameAsync(string name)
        {
            //    // Logic to get doctor by name



          //  var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Logic to get doctor by id


            //var patient = await _mediator.Send(new GetDoctorByNameQuery(name));

            //if (patient.UserId.ToString() != currentUserId && !User.IsInRole("Admin"))
            //    return Forbid();

            //return Ok(patient);


            var query = await _mediator.Send(new GetDoctorByNameQuery(name));
            return query is null ? NotFound() : Ok(query);

        }




        //create doctor

        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost("create")]
        public async Task<ActionResult<DoctorDTO>> Create(
    [FromBody] CreateDoctorCommand command,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            if (result is null)
                return BadRequest();

            return Ok(result);
        }



        [Authorize(Roles = "Doctor")]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrentDoctor(
        CancellationToken cancellationToken)
        {
            var userIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var doctor = await _mediator.Send(
                new GetDoctorByUserIdQuery(userId),
                cancellationToken
            );

            if (doctor is null)
                return NotFound("Doctor profile was not found.");

            return Ok(doctor);
        }









        [HttpPut("{id}", Name = "UpdateDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateDoctorDTO request)
        {
            var command = new UpdateDoctorCommand(id, request.Specialty, request.PersonId, request.ExperienceYears);

        var result=   await _mediator.Send(command);
            if(result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete(("{id:int}"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "unknown";

            if (id < 0)
            {
                _logger.LogWarning(
         "Admin action blocked (invalid id). AdminId={AdminId}, Action=DeleteStudent, TargetId={TargetId}, IP={IP}",
         adminId,
         id,
         ip);

                return BadRequest($"Not accepted ID {id}");
            }
            var command = new DeleteDoctorCommand(id);

            var result = await _mediator.Send(command);
            if (!result )
            {
                _logger.LogWarning(
            "Admin action blocked (invalid id). AdminId={AdminId}, Action=Deletedoctor, TargetId={TargetId}, IP={IP}",
            adminId,
            id,
            ip);

                _logger.LogWarning(
          "Admin action failed (target not found). AdminId={AdminId}, Action=Deletedoctor, TargetId={TargetId}, IP={IP}",
          adminId,
          id,
          ip
      );


                return BadRequest(result);
            }

            _logger.LogInformation(
         "Admin action succeeded. AdminId={AdminId}, Action=Deletedoctor, TargetId={TargetId}, IP={IP}",
         adminId,
         id,
         ip
     );
            return Ok(result);
        }




    }
}
