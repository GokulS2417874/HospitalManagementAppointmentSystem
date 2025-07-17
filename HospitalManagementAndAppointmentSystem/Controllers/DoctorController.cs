using Domain.Data;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using static Domain.Models.Enum;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin,Doctor,Patient,HelpDesk")]
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorRepo.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        [Authorize(Roles = "Admin,Doctor,Patient,HelpDesk")]
        [HttpGet("GetAllDoctorsById")]
        public async Task<IActionResult> GetDoctorByIdAsync(int id)
        {
            var doctors = await _doctorRepo.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        [Authorize(Roles = "Admin,HelpDesk")]
        [HttpGet("GetDoctorsByName")]
        public async Task<IActionResult> GetDoctorsByName(string name)
        {
            var doctors = await _doctorRepo.GetDoctorsByNameAsync(name);
            if (!doctors.Any()) return NotFound("Doctor not found");
            return Ok(doctors);
        }

        [Authorize(Roles = "Admin,HelpDesk")]

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
        [Authorize(Roles = "Admin")]
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
