using Domain.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorty
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public ReminderService(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppoingToken)
        {
            while (!stoppoingToken.IsCancellationRequested)
            {
                await CheckAndSendReminderAsync();
                await Task.Delay(TimeSpan.FromMinutes(30), stoppoingToken);
            }
        }

        private async Task CheckAndSendReminderAsync()
        {
            using var scope = _serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var emailservice = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            //var upcoming = await db.Appointments
            //               .Include(a => a.Patient)
            //               .Where(a => !a.IsReminderSent && a.AppointmentDateTime > DateTime.Now && a.AppointmentDateTime <= DateTime.Now.AddHours(2))
            //               .ToListAsync();



            var upcoming = await (from a in db.Appointments
                                  join p in db.Users on a.PatientId equals p.UserId
                                  where !a.IsReminderSent && p.Role == Domain.Models.Enum.UserRole.Patient
                                  && a.AppointmentStartTime > TimeOnly.FromDateTime(DateTime.Now) &&
                                  a.AppointmentStartTime <= TimeOnly.FromDateTime(DateTime.Now.AddHours(6))
                                  select new
                                  {
                                      a.AppointmentId,
                                      a.AppointmentStartTime,
                                      p.UserName,
                                      p.Email

                                  }
                                  ).ToListAsync();
                            

            foreach (var item in upcoming)
            {
                string subject = "Appointment Reminder";
                string body = $" Hello {item.UserName}"
                    + $" This is a reminder that you have an appointment on <b>{item.AppointmentStartTime}</b>.<br/>" + "Thank You!";
                await emailservice.SendEmailAsync(item.Email, subject, body);

                var appointment = await db.Appointments.FindAsync(item.AppointmentId);

                if (appointment != null)
                {
                    appointment.IsReminderSent = true;  
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
