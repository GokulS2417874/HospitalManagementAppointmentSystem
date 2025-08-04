using Domain.Models;
using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public  interface IUserRepository
    {
        Task<string> UpdateUserProfileAsync(string email, GenericRegistrationForm dto);
        Task<string> DeleteUserAsync(string email);
        Task<ICollection<Users>> GetAllUsersAsync();
        Task<Users?> FindByIdAsync(int id);

    }
}
