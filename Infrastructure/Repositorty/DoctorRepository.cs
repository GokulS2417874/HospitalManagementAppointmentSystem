﻿using Domain.Data;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using static Domain.Models.Enum;

namespace Infrastructure.Repositorty
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllDoctorsAsync() => await _context.Users
            .Where(d => d.Role == UserRole.Doctor)
            .Select(d => new
        {
            d.UserId,
            d.UserName,
            d.Email,
            d.PhoneNumber,
            d.Specialization,
            d.Qualification,
            d.ExperienceYears,
            d.Consultant_fees,
            d.Active_Status,
            d.EmergencyContactName,
            d.EmergencyContactPhoneNumber,
            d.EmergencyContactRelationship
        }).ToListAsync();

        public async Task<IEnumerable<object>> GetDoctorByIdAsync(int id)
        {
            return await _context.Users.Where(d => d.UserId == id && d.Role == UserRole.Doctor).Select(d => new
            {

                d.UserId,
                d.UserName,
                d.Email,
                d.PhoneNumber,
                d.Specialization,
                d.Qualification,
                d.ExperienceYears,
                d.Consultant_fees,
                d.Active_Status,
                d.EmergencyContactName,
                d.EmergencyContactPhoneNumber,
                d.EmergencyContactRelationship
            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetDoctorsByNameAsync(string name)
        {
            return await _context.Users.Where(d => d.UserName == name && d.Role == UserRole.Doctor)
                .Select(d => new {
                    d.UserId,
                    d.UserName,
                    d.Email,
                    d.PhoneNumber,
                    d.Specialization,
                    d.Qualification,
                    d.ExperienceYears,
                    d.Consultant_fees,
                    d.Active_Status,
                    d.EmergencyContactName,
                    d.EmergencyContactPhoneNumber,
                    d.EmergencyContactRelationship
                    }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetDoctorsBySpecializationAsync(specialization specialization)
        {
           
                return await _context.Users
                    .Where(d => d.Role == UserRole.Doctor && d.Specialization == specialization)
                    .Select(d => new
                    {
                        d.UserId,
                        d.UserName,
                        d.Specialization,
                        d.Qualification,
                        d.ExperienceYears,
                        d.Consultant_fees,
                        d.Active_Status,
                        d.EmergencyContactRelationship,
                        d.PhoneNumber
                    }).ToListAsync();

            
        }

        public async Task<Users> UpdateActiveStatus(string email, Status Status)
        {
            var EmployeeDetails = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.Role.Equals(UserRole.Patient));
            if (EmployeeDetails == null)
                return null;
            EmployeeDetails.Active_Status = Status;
            var curtime = TimeOnly.FromDateTime(DateTime.Now);
            if (EmployeeDetails.Active_Status.Equals(Status.Online) && EmployeeDetails.ShiftEndTime  < curtime)
            {
                EmployeeDetails.Active_Status = Status.Offline;
            }
            await _context.SaveChangesAsync();
            return EmployeeDetails;
        }



    }
}

