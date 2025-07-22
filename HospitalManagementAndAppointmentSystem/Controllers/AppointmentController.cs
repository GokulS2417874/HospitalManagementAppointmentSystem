using Domain.Data;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;
using Microsoft.AspNetCore.Authorization;


namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentController(IAppointmentRepository repo, AppDbContext context)
        {
            _repo = repo;
        }
        [Authorize(Roles = "Admin,HelpDesk,Patient")]
        [HttpGet("ListOfDoctors")]
        public async Task<IActionResult> DoctorsList(specialization specialization,ShiftTime Shift)
        {
            var Result = await _repo.GenerateDoctorSlots(specialization, Shift);

            return Ok(Result);

        }
        [Authorize(Roles = "Admin,Patient")]
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment([FromForm] AppointmentDto dto, specialization specialization, string Email,ShiftTime shift)
        {
            var Today = DateOnly.FromDateTime(DateTime.Today);
            var AppointmentDetails = await _repo.BookAppointment(dto, specialization, Email,shift);
            if(AppointmentDetails is string message)
            {
                return BadRequest(message);
            }
            else
            {
                return Ok(AppointmentDetails);
            }
            
            }

        [Authorize(Roles = "Admin,Patient")]
        [HttpPut("Reschedule")]
        public async Task<IActionResult> Reschedule([FromForm] RescheduledDto dto, string email)
        {
            var result = await _repo.RescheduleAppointmentAsync(email, dto);
            if (result == null)
                return BadRequest("Patient not found.");
            return Ok(result);
        }
        [Authorize(Roles = "Admin,Patient")]

        [HttpPut("Cancelled")]
        public async Task<IActionResult> Cancelled([FromForm] string Email)
        {
            var result = await _repo.CancelAppointmentAsync(Email);
            if (result == null)
                return BadRequest("Patient not found.");
            return Ok(result);
        }

        [Authorize(Roles = "Doctor,Admin")]

        [HttpGet("GetAppointmentsForDoctorId")]
        public async Task<IActionResult> ViewAppointmentsForDoctor([FromQuery] string Email)
        {
            var appointments = await _repo.GetAppointmentsForDoctorAsync(Email);

            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this doctor.");
            }

            return Ok(appointments);
        }

        [Authorize(Roles = "Doctor,Admin")]

        [HttpPut("UpdateAppointmentStatus-NotAttended-NotCompleted")]
        public async Task<IActionResult> UpdateAppointment( [FromForm]int AppointmentId,[FromForm] DoctorAppointmentUpdateDto dto)
        {
            var result = await _repo.UpdateAppointmentStatusAsync(AppointmentId, dto);
            if (result == "Patient not found.")
                return BadRequest(result);
            return Ok(result);
        }


        [Authorize(Roles = "Patient,Admin")]

        [HttpGet("GetAppointmentsByPatientId")]
        public async Task<IActionResult> ViewAppointmentsForPatient([FromQuery]int PatId)
        {
            var appointments = await _repo.GetAppointmentsByPatientIdAsync(PatId);

            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this doctor.");
            }

            return Ok(appointments);
        }
        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet("GetPatientMedicalHistoriesById")]
        public async Task<IActionResult> GetMedicalHistory(int id)
        {
            var result = await _repo.GetPatientMedicalHistoryAsync(id);

            if (result == null)
                return NotFound("No Medical History");

            return File(
            fileContents: result.Value.FilePath,
            contentType: result.Value.MimeType,
            fileDownloadName: result.Value.FileName
            );
        }
        [Authorize(Roles = "Admin,Doctor")]

        [HttpGet("Today-AppointmentsForDoctor")]
        public async Task<IActionResult> GetTodayAppointmentsForDoctor(int doctorId)
        {
            var appointments = await _repo.GetTodayAppointmentsForDoctorAsync(doctorId);

            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for today.");
            }

            return Ok(appointments);
        }

        }
    }
