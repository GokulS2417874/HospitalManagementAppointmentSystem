﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Patient : Users
    {
        public Patient()
        {
            Role = UserRole.Patient;
        }
        
        //public DateOnly? DateOfBirth { get; set; }
        //public PatientGender? Gender { get; set; }
        //public AppointmentType? AppointmentBookedBy { get; set; }
    }
}


   