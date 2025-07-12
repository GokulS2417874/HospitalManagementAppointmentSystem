using Domain.Data;
using Domain.Models;
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
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<object>> GetAllPatientsAsync() => await _context.Users
               .Where(p => p.Role == UserRole.Patient)
               .Select(p => new
               {
                   p.UserId,
                   p.UserName,
                   p.Email,
                   p.PhoneNumber,
                   p.EmergencyContactName,
                   p.EmergencyContactPhoneNumber,
                   p.EmergencyContactRelationship
               }).ToListAsync();

        public async Task<IEnumerable<object>> GetPatientByIdAsync(int id)
        {
            return await _context.Users.Where(p => p.UserId == id && p.Role == UserRole.Patient).Select(p => new
            {
                p.UserId,
                p.UserName,
                p.Email,
                p.PhoneNumber,
                p.EmergencyContactName,
                p.EmergencyContactPhoneNumber,
                p.EmergencyContactRelationship
            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPatientsByNameAsync(string name)
        {
            return await _context.Users.Where(p => p.UserName == name)
                .Select(p => new {
                    p.UserId,
                    p.UserName,
                    p.Email,
                    p.PhoneNumber,
                    p.EmergencyContactName,
                    p.EmergencyContactPhoneNumber,
                    p.EmergencyContactRelationship
                }).ToListAsync();
        }

    }
}
