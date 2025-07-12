using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Domain.Models.Enum;

namespace Infrastructure.Interface
{


    public interface IDoctorRepository
    {
        Task<IEnumerable<object>> GetAllDoctorsAsync();
        Task<IEnumerable<object>> GetDoctorByIdAsync(int id);
        Task<IEnumerable<object>> GetDoctorsByNameAsync(string name);
        Task<IEnumerable<object>> GetDoctorsBySpecializationAsync(specialization specialization);

      //  Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
    }
    }


