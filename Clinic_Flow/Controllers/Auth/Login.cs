using Clinic_Application.DTOs.Auth;
using Clinic_Application.Features.Auth.LoginCommand;
using Clinic_Application.Features.Auth.logout;
using Clinic_Application.Features.Auth.RefreshComand;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clinic_Flow.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {

        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> Login(loginCommand command)
        {
            var token = await _mediator.Send(command);

            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new 
            {
                token
            });
        }

        
        [HttpPost("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Refresh([FromBody] Refreshcommand request)
        { 

            var result = await _mediator.Send(request);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }       

            return Ok(new 
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }
       
        
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
