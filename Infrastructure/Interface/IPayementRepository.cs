using Domain.Models;
using Infrastructure.DTOs;

public interface IPayementRepository
{
    Task<Payment> CreatePaymentAsync(CreatePaymentDto dto);
    Task<List<Payment>> GetPaymentsByAppointmentAsync(int appointmentId);
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();

    // Add-on functions
    Task<decimal> GetTotalEarningsByDayAsync(DateTime date);
    Task<decimal> GetTotalEarningsByMonthAsync(int year, int month);
    Task<decimal> GetTotalEarningsByYearAsync(int year);
}
