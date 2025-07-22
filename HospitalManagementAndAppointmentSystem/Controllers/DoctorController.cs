using Domain.Data;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorRepository _doctorRepo;
        private readonly AppDbContext _context;

        public DoctorController(IDoctorRepository doctorRepo,AppDbContext context)
        {
            _doctorRepo = doctorRepo;
            _context = context;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepo.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("GetAllDoctorsById")]
        public async Task<IActionResult> GetDoctorByIdAsync(int id)
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
        [HttpPut("ActiveStatus")]
        public async Task<IActionResult> UpdateActiveStatus(string Email,Status status)
        {
            var EmployeeDetails = await _doctorRepo.UpdateActiveStatus(Email, status);
            if (EmployeeDetails == null)
                return BadRequest("EmployeeId not found");
            return Ok(EmployeeDetails);


        }

    }
}
