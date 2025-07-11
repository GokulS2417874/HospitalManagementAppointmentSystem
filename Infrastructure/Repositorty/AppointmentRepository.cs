using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }   
    public async Task<List<SlotWithDoctorDto>> GenerateDoctorSlots(specialization Specialization, ShiftTime Shift)
        {
            var Today = DateOnly.FromDateTime(DateTime.Today);
            var SlotDuration = TimeSpan.FromMinutes(30);
            var DoctorsDetails = await _context.Users.Where(q => q.Specialization.Equals(Specialization) && q.Shift.Equals(Shift) && q.Active_Status.Equals(Status.Online)).ToListAsync();
            var DoctorIds = _context.Appointments.Select(q => q.DoctorId).ToList();
            var AppointmentList = _context.Appointments.Where(a => DoctorIds.Contains(a.DoctorId) && a.AppointmentDate.Equals(Today)).ToList();
            TimeOnly StartTime = Shift
            switch
            {
                ShiftTime.Morning => new TimeOnly(5, 0),
                ShiftTime.Afternoon => new TimeOnly(13, 0),
                ShiftTime.Night => new TimeOnly(21, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(Shift), "Invalid shift")
            };
            var start = StartTime;
            var End = start.Add(SlotDuration);
            var isBooked = false;
            var result = new List<SlotWithDoctorDto>();
            foreach (var doctor in DoctorsDetails)
            {
                var BookedTimes = AppointmentList.Where(a => a.DoctorId == doctor.UserId)
                                                 .Select(a => a.AppointmentStartTime).ToHashSet();
                var Slots = new List<SlotDto>();
                var curtime = TimeOnly.FromDateTime(DateTime.Now);
                for (int i = 0; i < 16; i++)
                {
                    start = StartTime.AddMinutes(30 * i);
                    isBooked = BookedTimes.Contains(start);
                    End = start.Add(SlotDuration);
                    Slots.Add(new SlotDto
                    {
                        StartTime = start,
                        EndTime = End,
                        Shift = Shift,
                        IsBooked = isBooked,
                        Status = isBooked ? Status.Busy : (End < curtime) ? Status.Offline : Status.Online
                    });
                }
                result.Add(new SlotWithDoctorDto
                {
                    DoctorId = doctor.UserId,
                    DoctorName = doctor.UserName,
                    Specialization = doctor.Specialization,
                    Shift = Shift,
                    Slots = Slots
                });
            }
            return result;
        }
        public async Task<Appointment> BookAppointment(AppointmentDto dto, specialization specialization, string email)
        {
            var patientDetails = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Role == UserRole.Patient);

            if (patientDetails == null)
                return null; 

            if (!System.Enum.TryParse<specialization>(specialization.ToString(), true, out var Specialization))
                return null;

            var appointment = new Appointment
            {
                PatientId = patientDetails.UserId,
                PatientName = patientDetails.UserName,
                Specialization = Specialization,
                DoctorId = dto.DoctorId,
                DoctorName = dto.DoctorName,
                AppointmentDate = DateOnly.FromDateTime(DateTime.Today),
                AppointmentStartTime = dto.AppointmentStartTime,
                AppointmentEndTime = dto.AppointmentEndTime,
                AppointmentStatus = dto.Submit ? AppointmentStatus.Scheduled : AppointmentStatus.InProgress
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }
        public async Task<Appointment> RetrieveAppointmentDetails(string email)
        {
            

            var appointmentDetails = await _context.Users
                .Join(_context.Appointments,
                      u => u.UserId,
                      a => a.PatientId,
                      (u, a) => new { User = u, Appointment = a })
                .Where(q => q.User.Email == email &&
                            q.User.Role == UserRole.Patient &&
                            (q.Appointment.AppointmentStatus == AppointmentStatus.Scheduled|| q.Appointment.AppointmentStatus == AppointmentStatus.Rescheduled))
                .Select(q => q.Appointment)
                .FirstOrDefaultAsync();
            return appointmentDetails;

            
        }

    }
}






