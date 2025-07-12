using Domain.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class HelpDeskRepository : IHelpDeskRepository
    {
        private readonly AppDbContext _context;

        public HelpDeskRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
