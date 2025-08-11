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
  
    public async Task<Payment> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var appointment = await _context.Appointments.FindAsync(dto.AppointmentId);

        if (appointment == null)
        {
            throw new Exception("Appointment not Found");
        }

        var payment = new Payment
        {
            AppointmentId = appointment.AppointmentId,
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            PaidAmount = dto.PaidAmount,
            TotalAmount = dto.TotalAmount,
            PendingAmount = dto.TotalAmount - dto.PaidAmount,
            PaymentMethod = dto.PaymentMethod,
            PaymentDate = DateTime.Now
        };


        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<List<Payment>> GetPaymentsByAppointmentAsync(int appointmentId)
    {
        return await _context.Payments
            .Where(p => p.AppointmentId == appointmentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.ToListAsync();
    }


    public async Task<decimal> GetTotalEarningsByDayAsync(DateTime date)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Date == date.Date)
            .SumAsync(p => p.TotalAmount);
    }

    public async Task<decimal> GetTotalEarningsByMonthAsync(int year, int month)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
            .SumAsync(p => p.TotalAmount);
    }

    public async Task<decimal> GetTotalEarningsByYearAsync(int year)
    {
        return await _context.Payments
            .Where(p => p.PaymentDate.Year == year)
            .SumAsync(p => p.TotalAmount);
    }
}
