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
        Task<object> BookAppointment(AppointmentDto dto, specialization specialization, string email,ShiftTime shift);
        Task<Appointment> RetrieveAppointmentDetails(string Email);

        Task<(byte[] FilePath, string MimeType, string FileName)?> GetPatientMedicalHistoryAsync(int patientId);

        // Task<(byte[] FilePath, string MimeType, string FileName)?> GetMedicalHistoryAsync(string email);

        Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);

        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int doctorId);

        Task<List<Appointment>> GetTodayAppointmentsForDoctorAsync(int doctorId);


    }
}
