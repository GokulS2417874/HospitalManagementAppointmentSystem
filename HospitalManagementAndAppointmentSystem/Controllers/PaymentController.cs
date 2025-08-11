using Infrastructure.DTOs;
using Infrastructure.Interface;
using Infrastructure.Repositorty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayementRepository _paymentRepository;

        public PaymentController(IPayementRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("CreatePayment")]

        public async Task<IActionResult> CreatePayment([FromForm] CreatePaymentDto dto)
        {
            if (!System.Enum.IsDefined(typeof(PaymentMethod), dto.PaymentMethod))
            {
                return BadRequest("InValid Payment Method");
            }

            try
            {
                var payment = await _paymentRepository.CreatePaymentAsync(dto);
                return Ok(new
                {
                    payment.PatientId,
                    payment.DoctorId,
                    payment.PaymentId,
                    payment.TotalAmount,
                    payment.PaidAmount,
                    PendingAmount = payment.TotalAmount - payment.PaidAmount,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("Appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var payments = await _paymentRepository.GetPaymentsByAppointmentAsync(appointmentId);
            return Ok(payments);
        }

        [HttpGet("Payments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllPaymentsAsync();
            return Ok(payments);
        }



        // Earnings by Day
        [HttpGet("earnings/day")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEarningsByDay([FromQuery] DateTime date)
        {
            var total = await _paymentRepository.GetTotalEarningsByDayAsync(date);
            return Ok(new { Date = date.Date, TotalEarnings = total });
        }

        // Earnings by Month
        [HttpGet("earnings/month")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEarningsByMonth([FromQuery] int year, [FromQuery] int month)
        {
            var total = await _paymentRepository.GetTotalEarningsByMonthAsync(year, month);
            return Ok(new { Year = year, Month = month, TotalEarnings = total });
        }

        // Earnings by Year
        [HttpGet("earnings/year")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEarningsByYear([FromQuery] int year)
        {
            var total = await _paymentRepository.GetTotalEarningsByYearAsync(year);
            return Ok(new { Year = year, TotalEarnings = total });
        }
    }
}
