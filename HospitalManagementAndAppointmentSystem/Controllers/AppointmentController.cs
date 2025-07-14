using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Infrastructure.Repositorty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
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

        
        [HttpPut("ActiveStatus")]
        public async Task<IActionResult> UpdateActiveStatus(string Email)
        {
            var DoctorDetails = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            DoctorDetails.Active_Status = Status.Online;
            await _context.SaveChangesAsync();
            return Ok("Updated");
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
            var isAlreadyBooked = await _context.Appointments
                                                .AnyAsync(a => (a.AppointmentDate == Today && a.PatientId == AppointmentDetails.PatientId));
            var isSlotAlreadyBooked = await _context.Appointments.AnyAsync(a => a.DoctorId == AppointmentDetails.DoctorId
                                                                      && a.AppointmentDate == Today
                                                                      && a.AppointmentStartTime == AppointmentDetails.AppointmentStartTime);
            if(!isAlreadyBooked||AppointmentDetails.AppointmentStatus.Equals(AppointmentStatus.Cancelled)|| AppointmentDetails.AppointmentStatus.Equals(AppointmentStatus.NotAttended))
            {
                if (!isSlotAlreadyBooked)
                {
                    _context.Appointments.Add(AppointmentDetails);
                    _context.SaveChanges();
                    return Ok(AppointmentDetails);
                }
                else
                {
                    return BadRequest("Slot is Already Booked");
                }
            }
            else
            {
                return BadRequest("Patient already Booked an Appointment");
            }


            
        }
        //[Authorize(Roles = "Patient")]
        [HttpPut("Reschedule")]
        public async Task<IActionResult> Reschedule([FromForm] RescheduledDto dto,string Email)
        {
            //var Email = User.Identity.Name;
            var AppointmentDetails = await _repo.RetrieveAppointmentDetails(Email);

            if (AppointmentDetails == null)
            {
                return BadRequest("Patient not found.");
            }
            else
            {
                AppointmentDetails.AppointmentDate = DateOnly.FromDateTime(DateTime.Today);
                AppointmentDetails.AppointmentStartTime = dto.AppointmentStartTime;
                AppointmentDetails.AppointmentEndTime = dto.AppointmentEndTime;
                AppointmentDetails.AppointmentStatus = AppointmentStatus.Rescheduled;
            }
            await _context.SaveChangesAsync();
            return Ok(AppointmentDetails);
        }
        //[Authorize(Roles = "Patient")]
        [HttpPut("Cancelled")]
        public async Task<IActionResult> Cancelled([FromForm] CancelledDto dto)
        {
            var AppointmentDetails = await _repo.RetrieveAppointmentDetails(dto.Email);

            if (AppointmentDetails == null)
            {
                return BadRequest("Patient not found.");
            }
            else
            {
                AppointmentDetails.AppointmentDate = DateOnly.FromDateTime(DateTime.Today);
                AppointmentDetails.AppointmentStartTime = null;
                AppointmentDetails.AppointmentEndTime = null;
                AppointmentDetails.AppointmentStatus = AppointmentStatus.Cancelled;
            }
            await _context.SaveChangesAsync();
            return Ok(AppointmentDetails);
        }

        [HttpPut("UpdateAppointmentStatus")]
        public async Task<IActionResult> UpdateAppointment(string Email,DoctorAppointmentUpdateDto dto)
        {
            var AppointmentDetails = await _repo.RetrieveAppointmentDetails(Email);

            if (AppointmentDetails == null)
            {
                return BadRequest("Patient not found.");
            }
            else
            {
                AppointmentDetails.AppointmentStartTime = null;
                AppointmentDetails.AppointmentEndTime = null;
                AppointmentDetails.AppointmentStatus = dto.IsAttended && AppointmentDetails.AppointmentEndTime > TimeOnly.FromDateTime(DateTime.Now) ? AppointmentStatus.Completed : (AppointmentDetails.AppointmentEndTime < TimeOnly.FromDateTime(DateTime.Now) ? AppointmentStatus.NotAttended : AppointmentDetails.AppointmentStatus);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("GetAppointmentsByDoctorId")]
        public async Task<IActionResult> ViewAppointmentsForDoctor([FromQuery] int docId)
        {
            var appointments = await _repo.GetAppointmentsByDoctorIdAsync(docId);

            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this doctor.");
            }

            return Ok(appointments);
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