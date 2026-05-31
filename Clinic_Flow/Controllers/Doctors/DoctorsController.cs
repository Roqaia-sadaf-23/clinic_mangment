using Clinic_Application.DTOs.Doctor;
using Clinic_Application.Features.Doctor.Command.CreateDoctor;
using Clinic_Application.Features.Doctor.Command.DeleteDoctor;
using Clinic_Application.Features.Doctor.Command.UpdateDoctor;
using Clinic_Application.Features.Doctor.Queries.GetDoctor_;
using Clinic_Application.Features.Doctor.Queries.GetDoctorByID;
using Clinic_Application.Features.Doctor.Queries.GetDoctorByName;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic_Flow.Controllers.Doctors
{
  //  [Authorize]
    [ApiController]
    [Route("api/[controller]")]
     
    public class DoctorsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

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


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
                public  async Task<IActionResult> GetDoctorById(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Logic to get doctor by id


            var patient = await _mediator.Send(new GetDoctorByIdQuery(id));

            if (patient.UserId.ToString() != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            return Ok(patient);



            //var query = await _mediator.Send(new GetDoctorByIdQuery(id));
            //return query is null ? NotFound() : Ok(query);
        }

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

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorDTO>> Create(
         [FromBody] CreateDoctorCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return BadRequest();

            return Ok(result);
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




        [HttpDelete(("{id:int}"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {

            var command = new DeleteDoctorCommand(id);

            var result = await _mediator.Send(command);
            if (!result )
            {
                return BadRequest(result);
            }
            return Ok(result);
        }




    }
}
