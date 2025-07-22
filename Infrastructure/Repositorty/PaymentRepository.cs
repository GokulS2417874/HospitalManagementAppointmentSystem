using Domain.Data;
using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositorty
{
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
                PaidAmount = dto.PaidAmount,
                TotalAmount = dto.TotalAmount,
                PaymentMethod = dto.PaymentMethod,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }
        public async Task<List<Payment>> GetPaymentsByAppointmentAsync(int appointmentId)
        {
            return await _context.Payments.Where(a => a.AppointmentId == appointmentId).ToListAsync();
        }
    }
}

