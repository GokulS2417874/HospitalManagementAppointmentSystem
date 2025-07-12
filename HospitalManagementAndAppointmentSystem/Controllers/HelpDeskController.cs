using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelpDeskController : ControllerBase
    {
        private readonly IHelpDeskRepository _helpdeskRepo;

        public HelpDeskController(IHelpDeskRepository helpdeskRepo)
        {
            _helpdeskRepo = helpdeskRepo;
        }

        [HttpGet("GetAllHelpDesk")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _helpdeskRepo.GetAllHelpDeskAsync();
            return Ok(doctors);
        }

        [HttpGet("GetDoctorsByName")]
        public async Task<IActionResult> GetDoctorsByName(string name)
        {
            var doctors = await _helpdeskRepo.GetHelpDeskByNameAsync(name);
            if (!doctors.Any()) return NotFound("Doctor not found");
            return Ok(doctors);
        }

        [HttpGet("GetAllHelpdeskById")]
        public async Task<IActionResult> GetDoctorByIdAsync(int id)
        {
            var doctors = await _helpdeskRepo.GetHelpDeskByIdAsync(id);
            return Ok(doctors);
        }

    }
    }
