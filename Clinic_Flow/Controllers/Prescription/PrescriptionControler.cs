
using Clinic_Application.DTOs.Prescription;
 
using Clinic_Application.Features.Prescription.Command.CreatePrescription;
using Clinic_Application.Features.Prescription.Command.DeletePrescription;
using Clinic_Application.Features.Prescription.Command.UpdatePrescription;
using Clinic_Application.Features.Prescription.Query.GetAllPrescription;
using Clinic_Application.Features.Prescription.Query.GetPrescriptionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.Prescription
{ 
    //  [Authorize]
            [ApiController]
            [Route("api/[controller]")]
    public class PrescriptionControler(IMediator mediator) : ControllerBase
    {

  private readonly IMediator _mediator = mediator;

        [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public IActionResult GetPrescriptionInfo()
        {

            // Logic to get Prescription
            var query = _mediator.Send(new GetAllPrescriptionInfoQuery());
                return query is null ? NotFound() : Ok(query);
            }


            [HttpGet("{id:int}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> GetPrescriptionById(int id)
            {
                // Logic to get Prescription by id
                var query = await _mediator.Send(new GetPrescriptionByIdQuery(id));
                return query is null ? NotFound() : Ok(query);
            }

        //[HttpGet("by-name/{name}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetPrescriptionByName(string name)
        //{
        //    Logic to get Prescription by name
        //        var query = _mediator.Send(new GetPrescriptionByNameQuery(name));
        //    return query is null ? NotFound() : Ok(query);
        //}


            [HttpPost("create")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<ActionResult<CreatePrescriptionDTO>> Create(
             [FromBody] CreatePrescriptionCommand command,
               CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(command, cancellationToken);

                if (result < 0  )
                    return BadRequest();

                return Ok(result);
            }




            [HttpPut("{id}", Name = "UpdatePrescription")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Update(int id, UpdatePrescriptionDTO request)
            {
                var command = new UpdatePrescriptionCommand(id, request.MedicationName, request.Dosage, request.Frequency,request.SpecialInstructions);

                var result = await _mediator.Send(command);
                if (result is null)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }




            [HttpDelete("{id}", Name = "DeletePrescription")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> Delete(int id)
            {

                var command = new DeletePrescriptionCommand(id);

                var result = await _mediator.Send(command);
                if (!result)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }




        }
    }


