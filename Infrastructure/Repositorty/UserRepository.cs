
using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enum;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hash;

        public UserRepository(AppDbContext context, IPasswordHasher hash)
        {
            _context = context;
            _hash = hash;
        }

        public async Task<string> UpdateUserProfileAsync(string email, GenericRegistrationForm dto)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return "User not found.";
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.PasswordHash = _hash.Hash(dto.Password);

            user.PhoneNumber = dto.PhoneNumber;
            user.EmergencyContactName = dto.EmergencyContactName;
            user.EmergencyContactRelationship = dto.EmergencyContactRelationship;
            user.EmergencyContactPhoneNumber = dto.EmergencyContactPhoneNumber;
            user.DateOfBirth = dto.DateOfBirth;

            if (user.Role == UserRole.Doctor)
            {
                if (dto.Specialization.HasValue)
                    user.Specialization = dto.Specialization;

                if (!string.IsNullOrWhiteSpace(dto.Qualification))
                    user.Qualification = dto.Qualification;

                if (dto.ExperienceYears.HasValue)
                    user.ExperienceYears = dto.ExperienceYears.Value;

                //if (dto.Shift.HasValue)
                //    user.Shift = dto.Shift;
            }

            if (user.Role == UserRole.Patient)
            {
                if (!string.IsNullOrWhiteSpace(dto.Languages))
                    user.Languages = dto.Languages;

                //if (dto.Shift.HasValue)
                //    user.Shift = dto.Shift;
            }

            await _context.SaveChangesAsync();
            return "User profile updated successfully.";
        }

        public async Task<string> DeleteUserAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email== email);

            if (user == null)
            {
                return "User not found.";
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return "User deleted successfully.";
        }
         
        public async Task<ICollection<Users>> GetAllUsersAsync()
        
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }
    }
}
