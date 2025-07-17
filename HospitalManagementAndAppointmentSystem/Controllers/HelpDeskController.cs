using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;
using static Domain.Models.Enum;


namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelpDeskController : ControllerBase
    {
        private readonly IHelpDeskRepository _helpdeskRepo;
        private readonly AppDbContext _context;

        public HelpDeskController(IHelpDeskRepository helpdeskRepo,AppDbContext context)
        {
            _helpdeskRepo = helpdeskRepo;
            _context = context;
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
        [HttpPost("RegistrationByHelpDisk")]
        public async Task<IActionResult> RegistrationByHelpDesk([FromForm]GenericRegistrationForm form)
        {
            var PatientDetails = await _helpdeskRepo.RegistrationDoneByHelpDesk(form);
            if(PatientDetails==null)
            {
                return NotFound("Patient is not Registered by HelpDesk");

            }
            
            
            return Ok(PatientDetails);
        }

    }
    }
