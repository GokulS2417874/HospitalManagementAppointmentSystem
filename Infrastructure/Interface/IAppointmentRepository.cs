using Domain.Models;
using Infrastructure.DTOs;
using static Domain.Models.Enum;

namespace Infrastructure.Interface
{
    public interface IAppointmentRepository
    {
        Task<List<SlotWithDoctorDto>> GenerateDoctorSlots(specialization specialization, ShiftTime shift);
        Task<object> BookAppointment(AppointmentDto dto, specialization specialization, string email,ShiftTime shift);
        Task<List<Appointment>> GetAppointmentsForDoctorAsync(string email);
        Task<Appointment?> RescheduleAppointmentAsync(string email, RescheduledDto dto);
        Task<Appointment?> CancelAppointmentAsync(string email);
        Task<string> UpdateAppointmentStatusAsync(int AppointmentId, DoctorAppointmentUpdateDto dto);
        Task<Appointment> RetrieveAppointmentDetails(string Email);
        Task<(byte[] FilePath, string MimeType, string FileName)?> GetPatientMedicalHistoryAsync(int patientId);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int doctorId);
        Task<List<Appointment>> GetTodayAppointmentsForDoctorAsync(int doctorId);
    }
}
