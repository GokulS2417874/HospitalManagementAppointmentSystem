using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayementRepository _paymentRepo;

        public PaymentController(IPayementRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
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
                var payment = await _paymentRepo.CreatePaymentAsync(dto);
                return Ok(new
                {
                    payment.PaymentId,
                    payment.TotalAmount,
                    payment.PaidAmount,
                    PendingAmount = payment.TotalAmount - payment.PaidAmount,
                });
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

            [HttpGet("Appointment/{appointmentId}")]

            public async Task<IActionResult> GetByAppointment (int appointmentId)
        {
            var payments = await _paymentRepo.GetPaymentsByAppointmentAsync(appointmentId);
            return Ok(payments);
        }
     }
 }
    

