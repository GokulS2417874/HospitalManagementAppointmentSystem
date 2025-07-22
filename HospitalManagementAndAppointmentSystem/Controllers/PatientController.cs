using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;

        public PatientController(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }
        [Authorize(Roles = "Admin,Doctor,HelpDesk")]
        [HttpGet("GetAllPatient")]

        public async Task<IActionResult> GetAllPatient()
        {
            var patient = await _patientRepo.GetAllPatientsAsync();
            return Ok(patient);
        }
        [Authorize(Roles = "Admin,Doctor,HelpDesk")]
        [HttpGet("GetPatientByName")]
        public async Task<IActionResult> GetPatientsByName(string name)
        {
            var patient = await _patientRepo.GetPatientsByNameAsync(name);
            if (!patient.Any())
            {
                return NotFound("patient not found");
            }
            return Ok(patient);
        }
        [Authorize(Roles = "Admin,Doctor,Patient,HelpDesk")]
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> GetPatientsById(int id)
        {
            var patient = await _patientRepo.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound($"No patient found with ID.");
            }
            return Ok(patient);
        }
    }
}
