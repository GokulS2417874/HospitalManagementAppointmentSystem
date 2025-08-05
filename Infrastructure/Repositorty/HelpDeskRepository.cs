using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class HelpDeskRepository : IHelpDeskRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _hash;

        public HelpDeskRepository(AppDbContext context, IPasswordHasher hash)
        {
            _context = context;
            _hash = hash;
        }

        public async Task<IEnumerable<object>> GetAllHelpDeskAsync() => await _context.Users
          .Where(h => h.Role == UserRole.HelpDesk)
          .Select(h => new
          {
              h.UserId,
              h.UserName,
              h.Email,
              h.PhoneNumber,
              h.EmergencyContactName,
              h.EmergencyContactPhoneNumber,
              h.EmergencyContactRelationship,
              h.ProfileImage,
              h.ProfileImageFileName,
              h.ProfileImageMimeType
          }).ToListAsync();

        public async Task<object> GetHelpDeskByIdAsync(int id)
        {
            return await _context.Users.Where(h => h.UserId == id && h.Role == UserRole.HelpDesk).Select(h => new
            {
                h.UserId,
                h.UserName,
                h.Email,
                h.PhoneNumber,
                h.EmergencyContactName,
                h.EmergencyContactPhoneNumber,
                h.EmergencyContactRelationship,
                h.ProfileImage,
                h.ProfileImageFileName,
                h.ProfileImageMimeType
            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetHelpDeskByNameAsync(string name)
        {
            return await _context.Users.Where(h => h.UserName == name && h.Role == UserRole.HelpDesk)
                .Select(h => new {
                    h.UserId,
                    h.UserName,
                    h.Email,
                    h.PhoneNumber,
                    h.EmergencyContactName,
                    h.EmergencyContactPhoneNumber,
                    h.EmergencyContactRelationship,
                    h.ProfileImage,
                    h.ProfileImageFileName,
                    h.ProfileImageMimeType
                }).ToListAsync();
        }
        public async Task<Users> RegistrationDoneByHelpDesk(GenericRegistrationForm form)
        {
            Users user = new Users()
            {
                UserName = form.UserName,
                Email = form.Email,
                PasswordHash = _hash.Hash(form.Password),
                PhoneNumber = form.PhoneNumber,
                EmergencyContactName = form.EmergencyContactName,
                EmergencyContactRelationship = form.EmergencyContactRelationship,
                EmergencyContactPhoneNumber = form.EmergencyContactPhoneNumber,
                DateOfBirth = form.DateOfBirth,
                Gender = form.Gender ?? PatientGender.Others,
                Role = UserRole.Patient
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            user.RegisteredBy = AppointmentType.HelpDesk;
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<Users> UpdateActiveStatus(string email, Status Status)
        {
            var EmployeeDetails = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.Role.Equals(UserRole.Patient));
            if (EmployeeDetails == null)
                return null;
            EmployeeDetails.Active_Status = Status;
            var curtime = TimeOnly.FromDateTime(DateTime.Now);
            if (EmployeeDetails.Active_Status.Equals(Status.Online) && EmployeeDetails.ShiftEndTime < curtime)
            {
                EmployeeDetails.Active_Status = Status.Offline;
            }
            await _context.SaveChangesAsync();
            return EmployeeDetails;
        }
        public async Task<int> GetHelpDeskCountAsync()
        {
            return await _context.Users.CountAsync(d => d.Role == UserRole.HelpDesk);
        }
    }
}
