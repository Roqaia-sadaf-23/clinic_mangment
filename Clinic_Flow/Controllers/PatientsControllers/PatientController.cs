using Clinic_Application.DTOs.Patient;
using Clinic_Application.Features.Patients.Command.CreatePatient;
using Clinic_Application.Features.Patients.Command.DeletePatient;
using Clinic_Application.Features.Patients.Command.UpdatePatient;
using Clinic_Application.Features.Patients.Queries.GetPatient;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic_Flow.Controllers.PatientsControllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task< IActionResult> GetPatientInfo()
        {
            // Logic to get Patients
            var query = await _mediator.Send(new GetPatientInfoQuery());
            return query is null ? NotFound() : Ok(query);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDTO>> Create(
           [FromBody] CreatePatientCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{patientId}", Name = "GetPatientById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatientById(int patientId, CancellationToken cancellationToken)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await _mediator.Send(new GetPatientByIdQuery(patientId));

            if (patient.UserId.ToString() != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            return Ok(patient);
        


            //var result = await _mediator.Send(new GetPatientByIdQuery(id), cancellationToken);

            //return result is null ? NotFound() : Ok(result);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePatient(int id,  UpdatePatientDTO request)
        {
            var command = new UpdatePatientCommand (id, request.PersonId,request.BloodType);

          var result = await _mediator.Send(command);
            if (result is null)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePatient(int id)
        {
           
            var command = new DeletePatientCommand(id);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
