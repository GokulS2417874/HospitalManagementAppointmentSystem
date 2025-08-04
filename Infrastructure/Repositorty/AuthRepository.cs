using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enum;

namespace Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task AddUserAsync(Users User)
        {
            return _context.Users.AddAsync(User).AsTask();
        }
        public async Task<Users?> FindByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null) return user;

            return null;
        }
        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
        public async Task<string?> FindPassword(string Mail)
        {
            var LoginPassword = _context.Users.Where(q => q.Email.Equals(Mail))
               .Select(q => q.PasswordHash)
               .FirstOrDefault();
            return LoginPassword;
        }
        public async Task<Users?> FindByResetTokenAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ResetToken == token);
            if (user != null) return user;
            return null;

        }

        public async Task<Users?> GetAdminAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Admin)
                .FirstOrDefaultAsync();

        }
        public async Task<Users> RegisterUserAsync(GenericRegistrationForm form, string hashedPassword)
        {
            Users user = form.Role switch
            {
                UserRole.Admin => new Admin
                {
                    Email = form.Email,
                    PasswordHash = hashedPassword,
                    Role = UserRole.Admin
                },

                UserRole.Doctor => new Doctor
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = hashedPassword,
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    DateOfBirth = form.DateOfBirth,
                    Gender = form.Gender ?? PatientGender.Others,
                    Specialization = form.Specialization,
                    Qualification = form.Qualification,
                    ExperienceYears = form.ExperienceYears ?? 0,
                    Role = UserRole.Doctor
                },

                UserRole.Patient => new Patient
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = hashedPassword,
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    DateOfBirth = form.DateOfBirth,
                    Gender = form.Gender ?? PatientGender.Others,
                    Role = UserRole.Patient

                },

                UserRole.HelpDesk => new HelpDesk
                {
                    UserName = form.UserName,
                    Email = form.Email,
                    PasswordHash = hashedPassword,
                    PhoneNumber = form.PhoneNumber,
                    EmergencyContactName = form.EmergencyContactName,
                    EmergencyContactRelationship = form.EmergencyContactRelationship,
                    EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                    DateOfBirth = form.DateOfBirth,
                    Gender = form.Gender ?? PatientGender.Others,
                    Languages = form.Languages,
                    Qualification = form.Qualification,
                    Role = UserRole.HelpDesk
                },

                _ => null
            };

            if (user == null)
                throw new ArgumentException("Invalid role or missing required fields");

            // ✅ Handle profile image
            if (form.ProfileImage != null)
            {
                using var memoryStream = new MemoryStream();
                await form.ProfileImage.CopyToAsync(memoryStream);
                user.ProfileImage = memoryStream.ToArray();
                user.ProfileImageMimeType = form.ProfileImage.ContentType;
                user.ProfileImageFileName = form.ProfileImage.FileName;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }


    }
}
