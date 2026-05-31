using Clinic_Application.DTOs.Patient;
using Clinic_Application.DTOs.User;
using Clinic_Application.Features.Appointments.Query.GetAppointmentById;
using Clinic_Application.Features.Patients.Command.CreatePatient;
using Clinic_Application.Features.Patients.Command.DeletePatient;
using Clinic_Application.Features.Patients.Command.UpdatePatient;
using Clinic_Application.Features.Patients.Queries.GetPatientById;
using Clinic_Application.Features.people.Query.GetPeeple;
using Clinic_Application.Features.Users.Command.CreateUser;
using Clinic_Application.Features.Users.Command.DeleteUser;
using Clinic_Application.Features.Users.Command.UpdateUser;
using Clinic_Application.Features.Users.Query.GetUser;
using Clinic_Application.Features.Users.Query.GetUserByID;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic_Flow.Controllers.UsersController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    
    {

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetUsers()
        {
            // Logic to get Patients
            var query =await  _mediator.Send(new GetUsersQuery());
            return query is null ? NotFound() : Ok(query);
        }

        //[HttpPost("create")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<int>> Create(
        //   [FromBody] CreateUserCommand command,
        //   CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(command, cancellationToken);

        //    if (result <0)
        //        return BadRequest(result);

        //    return Ok(result);
        //}





        [HttpPost("register")]
             [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
{
    var result = await _mediator.Send(command);
            if (result is null ) return BadRequest();
            return Ok(result);
}




            [Authorize]

        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {


           
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId == null)
                    return Unauthorized();

                if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
                    return Forbid();

                var result = await _mediator.Send(new GetUserByIdQuery(id));

                return Ok(result);
            }
        //    var result = await _mediator.Send(new GetUsersByIdQuery(id));

        //    return result is null ? NotFound() : Ok(result);
        //}


        //[HttpPut("{Id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdateUser(UpdateUserDTO request)
        //{
        //    var command = new UpdateUserCommand(request.Id, request.Username, request.Email);

        //    await _mediator.Send(command);

        //    return NoContent();
        //}


        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int Id)
        {

            var command = new DeleteUserCommand(Id);

         var result=   await _mediator.Send(command);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
