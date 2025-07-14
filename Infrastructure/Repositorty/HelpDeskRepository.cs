using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
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
              h.EmergencyContactRelationship
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
                h.EmergencyContactRelationship
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
                    h.EmergencyContactRelationship
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
            return user;
        }
    }
}
