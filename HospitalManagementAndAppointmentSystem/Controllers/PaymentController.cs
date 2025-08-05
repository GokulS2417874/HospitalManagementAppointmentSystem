using Infrastructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
