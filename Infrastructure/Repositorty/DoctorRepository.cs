using Domain.Data;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllDoctorsAsync() => await _context.Users
            .Where(d => d.Role == UserRole.Doctor && d.IsApprovedByAdmin == AdminApproval.Approved)
            .Select(d => new
            {
                d.UserId,
                d.UserName,
                d.Email,
                d.PhoneNumber,
                d.Specialization,
                d.Qualification,
                d.ExperienceYears,
                d.Consultant_fees,
                d.Active_Status,
                d.EmergencyContactName,
                d.EmergencyContactPhoneNumber,
                d.EmergencyContactRelationship,
                d.ProfileImage,
                d.ProfileImageFileName,
                d.ProfileImageMimeType,
                d.Shift,                    // ← ADD THIS LINE
                d.ShiftStartTime,           // ← ADD THIS LINE
                d.ShiftEndTime

            }).ToListAsync();

        public async Task<IEnumerable<object>> GetDoctorByIdAsync(int id)
        {
            return await _context.Users.Where(d => d.UserId == id && d.Role == UserRole.Doctor).Select(d => new
            {

                d.UserId,
                d.UserName,
                d.Email,
                d.PhoneNumber,
                d.Specialization,
                d.Qualification,
                d.ExperienceYears,
                d.Consultant_fees,
                d.Active_Status,
                d.EmergencyContactName,
                d.EmergencyContactPhoneNumber,
                d.EmergencyContactRelationship,
                d.ProfileImage,
                d.ProfileImageFileName,
                d.ProfileImageMimeType,
                d.Shift,                    // ← ADD THIS LINE
                d.ShiftStartTime,           // ← ADD THIS LINE
                d.ShiftEndTime
            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetDoctorsByNameAsync(string name)
        {
            return await _context.Users.Where(d => d.UserName == name && d.Role == UserRole.Doctor)
                .Select(d => new {
                    d.UserId,
                    d.UserName,
                    d.Email,
                    d.PhoneNumber,
                    d.Specialization,
                    d.Qualification,
                    d.ExperienceYears,
                    d.Consultant_fees,
                    d.Active_Status,
                    d.EmergencyContactName,
                    d.EmergencyContactPhoneNumber,
                    d.EmergencyContactRelationship,
                    d.ProfileImage,
                    d.ProfileImageFileName,
                    d.ProfileImageMimeType,
                    d.Shift,                    // ← ADD THIS LINE
                    d.ShiftStartTime,           // ← ADD THIS LINE
                    d.ShiftEndTime
                }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetDoctorsBySpecializationAsync(specialization specialization)
        {

            return await _context.Users
                .Where(d => d.Role == UserRole.Doctor && d.Specialization == specialization)
                .Select(d => new
                {
                    d.UserId,
                    d.UserName,
                    d.Specialization,
                    d.Qualification,
                    d.ExperienceYears,
                    d.Consultant_fees,
                    d.Active_Status,
                    d.EmergencyContactRelationship,
                    d.PhoneNumber,
                    d.Shift,                    // ← ADD THIS LINE
                    d.ShiftStartTime,           // ← ADD THIS LINE
                    d.ShiftEndTime
                }).ToListAsync();


        }

        public async Task<Users> UpdateActiveStatus(string email, Status status)
        {
            var employeeDetails = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && !u.Role.Equals(UserRole.Patient));

            if (employeeDetails == null)
                return null;

            var currentTime = TimeOnly.FromDateTime(DateTime.Now);
            var start = employeeDetails.ShiftStartTime;
            var end = employeeDetails.ShiftEndTime;

            bool isWithinShift;

            if (start < end)
            {
                // Normal shift (e.g., 09:00 to 17:00)
                isWithinShift = currentTime >= start && currentTime <= end;
            }
            else
            {
                // Overnight shift (e.g., 21:00 to 05:00)
                isWithinShift = currentTime >= start || currentTime <= end;
            }

            employeeDetails.Active_Status = (status == Status.Online && isWithinShift)
                ? Status.Online
                : Status.Offline;

            await _context.SaveChangesAsync();
            return employeeDetails;
        }


        public async Task<int> GetDoctorCountAsync()
        {
            return await _context.Users.CountAsync(d => d.Role == UserRole.Doctor);
        }



    }
}

