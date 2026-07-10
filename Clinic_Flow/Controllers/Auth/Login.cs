using Azure.Core;
using Clinic_Application.DTOs.Auth;
using Clinic_Application.Features.Auth.LoginCommand;
using Clinic_Application.Features.Auth.logout;
using Clinic_Application.Features.Auth.RefreshComand;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Clinic_Flow.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableRateLimiting("AuthLimiter")]

        public async Task<IActionResult> Login(loginCommand command)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var token = await _mediator.Send(command);

            if (token == null)
            {
                _logger.LogWarning(
$"Failed login attempt (email or username not found). email or username={command.login}, IP={ip}",
command.login, ip);
             

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
        [EnableRateLimiting("AuthLimiter")]
        public async Task<IActionResult> Refresh([FromBody] Refreshcommand request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";


            var result = await _mediator.Send(request);
            if(!result.IsSuccess)
            {
                _logger.LogWarning(
                   $"Failed login attempt (email or username not found). email or username={request.Email}, IP={ip}",
                                request.Email, ip);

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
