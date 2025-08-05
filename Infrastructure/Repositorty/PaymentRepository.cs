using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

public class PaymentRepository : IPayementRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Payment
        {
            AppointmentId = dto.AppointmentId,
            Amount = dto.Amount,
            PaymentDate = DateTime.Now,
            PaymentMethod = dto.PaymentMethod,
            TransactionId = dto.TransactionId
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Payment>> GetPaymentsByAppointmentAsync(int appointmentId)
    {
        return await _context.Payments
            .Where(p => p.AppointmentId == appointmentId)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalEarningsByDayAsync(DateTime date)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Date == date.Date)
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetTotalEarningsByMonthAsync(int year, int month)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetTotalEarningsByYearAsync(int year)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Year == year)
            .SumAsync(p => p.Amount);
    }
}
