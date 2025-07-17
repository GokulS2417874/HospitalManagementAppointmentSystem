using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
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
    }
}
