  using Clinic_Application.DTOs.MedicalRecord;
 
using Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Command.DeleteMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Command.UpdateMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Query.GetAllMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Query.GetByIdMedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Flow.Controllers.MedicalRecord
{   
    
    //  [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordContreoler : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalRecordContreoler(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllMedicalRecordInfo()
        {
            // Logic to get doctors
            var query = _mediator.Send(new GetAllMedicalRecordQuery());
            return query is null ? NotFound() : Ok(query);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
            // Logic to get doctor by id
            var query = await _mediator.Send(new GetByIdMedicalRecordQuery(id));
            return query is null ? NotFound() : Ok(query);
        }

    


        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateMedicalRecordDTO>> Create(
         [FromBody] CreateMedicalRecordCommand command,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result < 0)
                return BadRequest();

            return Ok(result);
        }




        [HttpPut("{id}", Name = "UpdateMedicl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateDiagnosisMedicalRecordDTO request)
        {
            var command = new UpdateDiagnosisMedicalRecordCommand(id, request.Diagnosis, request.Notes, request.VisitDescription,
                request.AppointmentId,request.PaymentId,request.PrescriptionId);

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

            var command = new DeleteMedicalRecordCommand(id);

            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



    }
}
