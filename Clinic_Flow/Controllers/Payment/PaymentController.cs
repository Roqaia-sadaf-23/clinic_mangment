
using Clinic_Application.DTOs.Payment;

using Clinic_Application.Features.Payment.Command.CreatePayment;
using Clinic_Application.Features.Payment.Command.DeletePayment;
using Clinic_Application.Features.Payment.Command.UpdatePayment;
using Clinic_Application.Features.Payment.Query.GetAllPayment;
using Clinic_Application.Features.Payment.Query.GetPaymentByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.Payment
{   
    
    //  [Authorize]
    [ApiController]
    [Route("api/[controller]")]


    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllPaymentInfo()
        {
            // Logic to get doctors
            var query = _mediator.Send(new GetPaymentInfoQuery());
            return query is null ? NotFound() : Ok(query);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            // Logic to get doctor by id
            var query = await _mediator.Send(new GetPaymentByIdQuery(id));
            return query is null ? NotFound() : Ok(query);
        }




        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreatePaymentDTO>> Create(
         [FromBody] CreatePaymentCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result < 0)
                return BadRequest();

            return Ok(result);
        }




        [HttpPut("{id}", Name = "UpdateMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdatePaymentDTO request)
        {
            var command = new UpdatePaymentCommand(id, request.amount, request.paymentMethod, request.Note  );

            var result = await _mediator.Send(command);
            if (result is null)
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

            var command = new DeletePaymentCommand(id);

            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
