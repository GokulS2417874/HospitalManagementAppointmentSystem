﻿using Domain.Models;
using Infrastructure.DTOs;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static Domain.Models.Enum;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin,Helpdesk")]
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
        [Authorize(Roles = "Admin,Doctor,Patient,Helpdesk")]
        [HttpGet("Appointment/{appointmentId}")]

            public async Task<IActionResult> GetByAppointment (int appointmentId)
        {
            var payments = await _paymentRepo.GetPaymentsByAppointmentAsync(appointmentId);
            return Ok(payments);
        }
     }
 }
    

