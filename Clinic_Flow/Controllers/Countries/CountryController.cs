using Clinic_Application.Features.Country.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.Countries
{
    [ApiController]
    [Route("api/[controller]")]

    public class CountryController: ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCountries()
        {
            // Logic to get doctors
            var query = _mediator.Send(new GetCountryQuery());
            return query is null ? NotFound() : Ok(query);

        }

    }
}
