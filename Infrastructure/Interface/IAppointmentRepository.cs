using Domain.Models;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Infrastructure.Interface
{
    public interface IAppointmentRepository
    {
        Task<List<SlotWithDoctorDto>> GenerateDoctorSlots(specialization specialization, ShiftTime shift);
        Task<Appointment> BookAppointment(AppointmentDto dto, specialization specialization, string email);
        Task<Appointment> RetrieveAppointmentDetails(string Email);
    }
}
