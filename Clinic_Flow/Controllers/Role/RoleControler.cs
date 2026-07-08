using Clinic_Application.Features.Payment.Query.GetAllPayment;
using Clinic_Application.Features.Role.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.Role
{
    [ApiController]
    [Route("api/[controller]")]

    public class Role(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoles()
        {
            // Logic to get doctors
            var query = await mediator.Send(new GetRolesQuery());
            return query is null ? NotFound() : Ok(query);
        }

    }
}
