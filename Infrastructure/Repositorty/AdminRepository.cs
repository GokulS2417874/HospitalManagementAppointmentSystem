using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interface;
using Domain.Models;
using Domain.Data;
using static Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorty
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Users?>> ApprovedByAdmin()
        {
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient)).ToListAsync();
            return EmployeeDetailsList;
        }
    }
}
