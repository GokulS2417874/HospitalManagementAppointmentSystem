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
                ShiftTime.Morning => new TimeOnly(5,0),
                ShiftTime.Afternoon => new TimeOnly(13,0),
                ShiftTime.Night => new TimeOnly(21,0),
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
                        Status = isBooked ? SlotStatus.Busy : (End < curtime) ? SlotStatus.Not_Available : SlotStatus.Available
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
        public async Task<object> BookAppointment(AppointmentDto dto, specialization specialization, string email, ShiftTime shift)
        {
            byte[]? filebytes = null;
            if (dto.FilePath != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.FilePath.CopyToAsync(memoryStream);
                    filebytes = memoryStream.ToArray();
                }

            }

            var patientDetails = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Role == UserRole.Patient);

            if (patientDetails == null)
                return null;
            var Today = DateOnly.FromDateTime(DateTime.Today);

            if (!System.Enum.TryParse<specialization>(specialization.ToString(), true, out var Specialization))
                return null;

            var AppointmentDetails = new Appointment
            {
                PatientId = patientDetails.UserId,
                PatientName = patientDetails.UserName,
                Specialization = Specialization,
                DoctorId = dto.DoctorId,
                DoctorName = dto.DoctorName,
                AppointmentDate = DateOnly.FromDateTime(DateTime.Today),
                AppointmentStartTime = dto.AppointmentStartTime,
                AppointmentEndTime = dto.AppointmentEndTime,
                AppointmentStatus = dto.Submit ? AppointmentStatus.Scheduled : AppointmentStatus.InProgress,
                FilePath = filebytes,
                FileName = dto.FilePath?.FileName,
                MimeType = dto.FilePath?.ContentType,
            };
            var isAlreadyBooked = await _context.Appointments
                                                .AnyAsync(a => (a.AppointmentDate == Today && a.PatientId == AppointmentDetails.PatientId));
            var Appt = await _context.Appointments
                                                .FirstOrDefaultAsync(a => (a.AppointmentDate == Today && a.PatientId == AppointmentDetails.PatientId));
            var isSlotAlreadyBooked = await _context.Appointments.AnyAsync(a => a.DoctorId == AppointmentDetails.DoctorId
                                                                      && a.AppointmentDate == Today
                                                                      && a.AppointmentStartTime == AppointmentDetails.AppointmentStartTime);
            if (!isAlreadyBooked || Appt.AppointmentStatus.Equals(AppointmentStatus.Cancelled) || Appt.AppointmentStatus.Equals(AppointmentStatus.NotAttended))
            {
                if (!isSlotAlreadyBooked)
                {
                    _context.Appointments.Add(AppointmentDetails);
                    _context.SaveChanges();
                    return (AppointmentDetails);
                }
                else
                {
                    return "Slot is Already Booked";
                }
            }
            else
            {
                return "Patient already Booked an Appointment";
            }

            
        }
        public async Task<Appointment?> CancelAppointmentAsync(string email)
        {
            var appointment = await RetrieveAppointmentDetails(email);

            if (appointment == null)
                return null;

            appointment.AppointmentDate = DateOnly.FromDateTime(DateTime.Today);
            appointment.AppointmentStartTime = null;
            appointment.AppointmentEndTime = null;
            appointment.AppointmentStatus = AppointmentStatus.Cancelled;

            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<Appointment?> RescheduleAppointmentAsync(string email, RescheduledDto dto)
        {
            var appointment = await RetrieveAppointmentDetails(email);

            if (appointment == null)
                return null;

            appointment.AppointmentDate = DateOnly.FromDateTime(DateTime.Today);
            appointment.AppointmentStartTime = dto.AppointmentStartTime;
            appointment.AppointmentEndTime = dto.AppointmentEndTime;
            appointment.AppointmentStatus = AppointmentStatus.Rescheduled;

            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<string> UpdateAppointmentStatusAsync(int id, DoctorAppointmentUpdateDto dto)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(appointment=>appointment.AppointmentId==id);

            if (appointment == null)
                return "Patient not found.";

            appointment.AppointmentStartTime = null;
            appointment.AppointmentEndTime = null;

            var now = TimeOnly.FromDateTime(DateTime.Now);
            appointment.AppointmentStatus = dto.IsAttended && appointment.AppointmentEndTime > now
                ? AppointmentStatus.Completed
                :AppointmentStatus.NotAttended ;

            await _context.SaveChangesAsync();
            return "Appointment status updated.";
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
                            (q.Appointment.AppointmentStatus == AppointmentStatus.Scheduled || q.Appointment.AppointmentStatus == AppointmentStatus.Rescheduled))
                .Select(q => q.Appointment)
                .FirstOrDefaultAsync();
            return appointmentDetails;

        }

        public async Task<(byte[] FilePath, string MimeType, string FileName)?> GetPatientMedicalHistoryAsync(int patientId)
        {
            var appointment = await _context.Appointments
            .Where(a => a.PatientId == patientId).
            OrderByDescending(a=>a.AppointmentDate)
            .Select(a => new
            {
                a.FilePath,
                a.MimeType,
                a.FileName
            })
            .SingleOrDefaultAsync();

            if (appointment == null || appointment.FilePath == null)
                return null;

            return (appointment.FilePath, appointment.MimeType ?? "application/pdf", appointment.FileName ?? "MedicalHistory.pdf");

        }

        public async Task<List<Appointment>> GetAppointmentsForDoctorAsync(string email)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            return await _context.Users
                .Join(_context.Appointments,
                      u => u.UserId,
                      a => a.DoctorId,
                      (u, a) => new { User = u, Appointment = a })
                .Where(q => q.User.Email == email &&
                            q.User.Role == UserRole.Doctor &&
                            (q.Appointment.AppointmentStatus == AppointmentStatus.Scheduled))
                .Select(q => q.Appointment)
                .ToListAsync();
        }


        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int PatientId)
        {
            return await _context.Appointments
            .Where(a => a.PatientId ==  PatientId)
            .ToListAsync();
        }

        public async Task<List<Appointment>> GetTodayAppointmentsForDoctorAsync(int doctorId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == today)
                .ToListAsync();
        }

    }
}





