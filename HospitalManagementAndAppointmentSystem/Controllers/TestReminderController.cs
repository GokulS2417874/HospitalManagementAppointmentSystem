using Domain.Data;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAndAppointmentSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestReminderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailService;

        public TestReminderController(AppDbContext context, IEmailSender emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("send-reminder/{appointmentId}")]
        public async Task<IActionResult> sendManualReminder(int appointmentId)
        {
            //var appointment = await _context.Appointments
            //    .Include(a => a.Patient).FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

            var appointment = await (from a in _context.Appointments
                                     join p in _context.Users on a.PatientId equals p.UserId
                                     where a.AppointmentId == appointmentId
                                     select new
                                     {
                                         Appointment = a,
                                         Patient = p
                                     }
                                     ).FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound("Appointment not found");
            }

            string subject = "Appointment Reminder";
            string body = $" Hello{appointment.Patient.UserName} User"
                + $"This is a reminder that you have an appointment on <b>{appointment.Appointment.AppointmentStartTime}</b>.<br/>" + "Thank You!";
            await _emailService.SendEmailAsync(appointment.Patient.Email, subject, body);


            appointment.Appointment.IsReminderSent = true;
            await _context.SaveChangesAsync();

            return Ok("Reminder sent Successfully");
        }
    }
}
