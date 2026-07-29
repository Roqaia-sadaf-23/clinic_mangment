  using Clinic_Application.DTOs.MedicalRecord;
using Clinic_Application.Features.MedicalRecord.Query.GetDoctorPatientMedicalRecord;

using Clinic_Application.Features.MedicalRecord.Command.CreateMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Command.DeleteMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Command.UpdateMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Query.GetAllMedicalRecord;
using Clinic_Application.Features.MedicalRecord.Query.GetByIdMedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [HttpGet("{id:int},GetMedicalRecordById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
            // Logic to get doctor by id
            var query = await _mediator.Send(new GetByIdMedicalRecordQuery(id));
            return query is null ? NotFound() : Ok(query);
        }



        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateMedicalRecordDTO dto,
            CancellationToken cancellationToken)
        {
            var userIdValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var medicalRecordId = await _mediator.Send(
                new CreateMedicalRecordCommand(
                    userId,
                    dto.AppointmentId,
                    dto.Diagnosis,
                    dto.VisitDescription,
                    dto.Notes),
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = medicalRecordId },
                new
                {
                    id = medicalRecordId,
                    message = "Medical record created successfully."
                });
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            // يمكنك تنفيذ Query لاحقًا.
            return Ok();
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/me/patients/{patientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorPatientMedicalRecords(
    int patientId,
    CancellationToken cancellationToken)
        {
            if (!TryGetCurrentUserId(out var userId))
                return Unauthorized();

            var result = await _mediator.Send(
                new GetDoctorPatientMedicalRecordsQuery(
                    userId,
                    patientId),
                cancellationToken);

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


        private bool TryGetCurrentUserId(out int userId)
        {
            var userIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userIdValue, out userId);
        }

    }
}
