using Domain.Data;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        private readonly AppDbContext _context;
        public AppointmentController(IAppointmentRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        [HttpGet("ListOfDoctors")]
        public async Task<IActionResult> DoctorsList(specialization specialization,ShiftTime Shift)
        {
            var Result = await _repo.GenerateDoctorSlots(specialization, Shift);

            return Ok(Result);

        }
        //[Authorize(Roles = "Patient")]
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

        //[Authorize(Roles = "Patient")]
        [HttpPut("Reschedule")]
        public async Task<IActionResult> Reschedule([FromForm] RescheduledDto dto, string email)
        {
            var result = await _repo.RescheduleAppointmentAsync(email, dto);
            if (result == null)
                return BadRequest("Patient not found.");
            return Ok(result);
        }

        [HttpPut("Cancelled")]
        public async Task<IActionResult> Cancelled([FromForm] string Email)
        {
            var result = await _repo.CancelAppointmentAsync(Email);
            if (result == null)
                return BadRequest("Patient not found.");
            return Ok(result);
        }


        [HttpGet("GetAppointmentsForDoctor")]
        public async Task<IActionResult> ViewAppointmentsForDoctor([FromQuery] string Email)
        {
            var appointments = await _repo.GetAppointmentsForDoctorAsync(Email);

            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this doctor.");
            }

            return Ok(appointments);
        }


        [HttpPut("UpdateAppointmentStatus-NotAttended-NotCompleted")]
        public async Task<IActionResult> UpdateAppointment( [FromForm]int AppointmentId,[FromForm] DoctorAppointmentUpdateDto dto)
        {
            var result = await _repo.UpdateAppointmentStatusAsync(AppointmentId, dto);
            if (result == "Patient not found.")
                return BadRequest(result);
            return Ok(result);
        }



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
//if (!System.Enum.TryParse<specialization>(specialization.ToString(), true, out var Specialization))
//        return BadRequest("Invalid Specialization");
//    var DoctorDetails = _context.Users.Where(x => x.Role.Equals(UserRole.Doctor) && x.Specialization.Equals(specialization)&&x.Active_Status.Equals(Status.Online))
//                                                   .GroupBy(x => x.Specialization)
//                                                   .Select(group => new
//                                                   {
//                                                       Specialization = group.Key,
//                                                       Doctors = group.OrderByDescending(x => x.UserName)
//                                                   .Select(x => new DoctorDto
//                                                   {
//                                                       DoctorId = x.UserId,
//                                                       DoctorName = x.UserName,
//                                                       //Specialization = Specialization.ToString()
//                                                   }).ToList()
//                                                   }).ToList();
//var OpeningTime = new TimeOnly(9, 0, 0);
//var ClosingTime = new TimeOnly(17, 0, 0);
//if (dto.AppointmentTime < OpeningTime || dto.AppointmentTime > ClosingTime)
//{
//    return BadRequest("Appointment Must be Scheduled Between 9AM AND 5PM");
//}
//else
//{
//}
//                //var OpeningTime = new TimeOnly(9, 0, 0);
//                //var ClosingTime = new TimeOnly(17, 0, 0);
//                //if (dto.AppointmentTime < OpeningTime || dto.AppointmentTime > ClosingTime)
//                //{
//                //    return BadRequest("Appointment Must be Scheduled Between 9AM AND 5PM");
//                //}
//[HttpGet("Getmedical-history")]
//public async Task<IActionResult> GetMedicalHistory(string email)
//{
//    var result = await _repo.GetMedicalHistoryAsync(email);

//    if (result == null)
//        return NotFound("No Medical History");

//    return File(
//    fileContents: result.Value.FilePath,
//    contentType: result.Value.MimeType,
//    fileDownloadName: result.Value.FileName
//    );

//}