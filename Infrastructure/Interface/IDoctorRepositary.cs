using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.Interface
{


    public interface IDoctorRepository
    {
        Task<IEnumerable<object>> GetAllDoctorsAsync();
        Task<object> GetDoctorByIdAsync(int id);
        Task<IEnumerable<object>> GetDoctorsByNameAsync(string name);
       // Task<IEnumerable<object>> GetDoctorsBySpecializationAsync(string specialization);
    }
    }


