using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionController(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("AddPrescription")]
        public async Task<IActionResult> AddPrescription([FromForm] PrescriptionDto dto)
        {
            var result = await _repository.AddPrescriptionAsync(dto);
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }

        //[HttpPut("updatePrescription/{id}")]
        //public async Task<IActionResult> UpdatePrescription(int id, [FromForm] PrescriptionDto dto)
        //{
        //    var result = await _repository.UpdatePrescriptionAsync(id, dto);
        //    if (result.Success)
        //        return Ok(result.Message);
        //    return NotFound(result.Message);
        //}

        [HttpDelete("deletePrescription/{id}")]
        public async Task<IActionResult> DeletePrescription([FromForm] int id)
        {
            var result = await _repository.DeletePrescriptionAsync(id);
            if (result.Success)
                return Ok(result.Message);
            return NotFound(result.Message);
        }

        [HttpGet("patientPrescription/{patientId}")]
        public async Task<IActionResult> GetPrescriptionsByPatient([FromRoute] int patientId)
        {
            var prescriptions = await _repository.GetPrescriptionsByPatientAsync(patientId);
            return Ok(prescriptions);
        }

        [HttpGet("GetPrescriptionbyID/{id}")]
        public async Task<IActionResult> GetPrescriptionById([FromRoute] int id)
        {
            var prescription = await _repository.GetPrescriptionByIdAsync(id);
            if (prescription == null)
                return NotFound("Prescription not found.");
            return Ok(prescription);
        }

        //[HttpGet]

        //public async Task<PrescriptionDto?> GetPrescriptionByIdAsync(int PerscriptionId)
        //{
        //    var prescription = await _repository
        //}
    }
}
