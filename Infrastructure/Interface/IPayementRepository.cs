using Domain.Models;
using Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IPayementRepository
    {
        Task<Payment> CreatePaymentAsync(CreatePaymentDto dto);

        Task<List<Payment>> GetPaymentsByAppointmentAsync(int appointmentId);
    }
}
