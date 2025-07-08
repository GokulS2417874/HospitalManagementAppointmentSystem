using Domain.Data;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorty
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
    }
}
