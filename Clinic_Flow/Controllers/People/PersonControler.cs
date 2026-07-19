using Clinic_Application.DTOs.Patient;
using Clinic_Application.DTOs.Person;
using Clinic_Application.Features.Patients.Command.CreatePatient;
using Clinic_Application.Features.Patients.Command.DeletePatient;
using Clinic_Application.Features.Patients.Command.UpdatePatient;
using Clinic_Application.Features.Patients.Queries.GetPatient;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Application.Features.people.Command.CreatePerson;
using Clinic_Application.Features.people.Command.DeletePerson;
using Clinic_Application.Features.people.Command.UpdateImage;
using Clinic_Application.Features.people.Command.UpdatePerson;
using Clinic_Application.Features.people.Query.GetPeeple;
using Clinic_Application.Features.people.Query.GetPersonById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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



        // PUT: api/People/me/image
        [Authorize(Roles = "Doctor,Patient")]
        [HttpPut("me/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMyImage(
            [FromBody] UpdateImageRequest request,
            CancellationToken cancellationToken)
        {
            var userIdValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            if (string.IsNullOrWhiteSpace(request.ImagePath))
                return BadRequest("Image path is required.");

            var updated = await _mediator.Send(
                new UpdateImageCommand(
                    UserId: userId,
                    ImagePath: request.ImagePath
                ),
                cancellationToken
            );

            if (!updated)
                return NotFound(
                    "Person profile was not found."
                );

            return Ok(new
            {
                imagePath = request.ImagePath,
                message = "Profile image updated successfully."
            });
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
