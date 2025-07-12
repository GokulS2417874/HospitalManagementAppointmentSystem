using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.Interface
{
    public interface IPatientRepository
    {
        Task<IEnumerable<object>> GetAllPatientsAsync();
        Task<IEnumerable<object>> GetPatientByIdAsync(int id);
        Task<IEnumerable<object>> GetPatientsByNameAsync(string name);
        }
}
