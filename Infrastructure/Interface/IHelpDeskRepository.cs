using Domain.Models;
using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.Interface
{
    public interface IHelpDeskRepository
    {
        Task<IEnumerable<object>> GetAllHelpDeskAsync();
        Task<object> GetHelpDeskByIdAsync(int id);
        Task<IEnumerable<object>> GetHelpDeskByNameAsync(string name);
        Task<Users> RegistrationDoneByHelpDesk(GenericRegistrationForm form);
        Task<Users> UpdateActiveStatus(string email, Status Status);
        Task<int> GetHelpDeskCountAsync();
        
    }
}
