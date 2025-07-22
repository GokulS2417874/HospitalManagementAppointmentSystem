using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IAuthRepository
    {
        Task AddUserAsync(Users User);
        Task<Users?> FindByEmailAsync(string email);
        Task SaveAsync();
        Task<string?> FindPassword(string Mail);
        Task<Users?> FindByResetTokenAsync(string token);
        Task<Users?> GetAdminAsync();
    }
}
