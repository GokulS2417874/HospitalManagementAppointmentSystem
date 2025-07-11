using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorRepository _doctorRepo;

        public DoctorController(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        [HttpGet("GetAllDoctors")]
      
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepo.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("GetDoctorsByName")]
        public async Task<IActionResult> GetDoctorsByName(string name)
        {
            var doctors = await _doctorRepo.GetDoctorsByNameAsync(name);
            if (!doctors.Any()) return NotFound("Doctor not found");
            return Ok(doctors);
        }

        [HttpGet("GetDoctorsBySpecialization")]
        public async Task<IActionResult> GetDoctorsBySpecialization([FromQuery] specialization  Specializaition)
        {
            var doctors = await _doctorRepo.GetDoctorsBySpecializationAsync(Specializaition);
            if (!doctors.Any()) 
            {
                return NotFound("Doctor not found for Specialization");
             }
            return Ok(doctors);
        }

    }
}
