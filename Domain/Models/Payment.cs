﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Enum;

namespace Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public Users Patient { get; set; }
        public Appointment Appointment { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PendingAmount => TotalAmount - PaidAmount;
        public DateTime PaymentDate {  get; set; } = DateTime.Now;
        public PaymentMethod PaymentMethod { get; set; }

    }
}
