using Clinic_Application.DTOs.Patient;
using Clinic_Application.DTOs.Person;
using Clinic_Application.Features.Patients.Command.CreatePatient;
using Clinic_Application.Features.Patients.Command.DeletePatient;
using Clinic_Application.Features.Patients.Command.UpdatePatient;
using Clinic_Application.Features.Patients.Queries.GetPatient;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Application.Features.people.Command.CreatePerson;
using Clinic_Application.Features.people.Command.DeletePerson;
using Clinic_Application.Features.people.Command.UpdatePerson;
using Clinic_Application.Features.people.Query.GetPeeple;
using Clinic_Application.Features.people.Query.GetPersonById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.PeopleControler
{
    [ApiController]
    [Route("api/[controller]")]
    public class Person : ControllerBase
    {

        private readonly IMediator _mediator;

        public Person(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetPeople()
        {
            // Logic to get Patients
            var query = _mediator.Send(new GetPeopleQuery());
            return query is null ? NotFound() : Ok(query);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDTO>> Create(
           [FromBody] CreatePersonCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpGet("{id}", Name = "GetPersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPersonByIdQuery(id));

            return result is null ? NotFound() : Ok(result);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePerson(int id,UpdatePersonDTO request)
        {
            var command = new UpdatePersonCommand(id, request.FirstName, request.LastName,request.NationalityNo,
                request.PhoneNumber, request.Age,
                request.Address, request.Gender , request.NationalityCountryId, request.ImagePath,request.Note);

          var result = await _mediator.Send(command);
            if (result is null)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var command = new DeletePersonCommand(id);

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
