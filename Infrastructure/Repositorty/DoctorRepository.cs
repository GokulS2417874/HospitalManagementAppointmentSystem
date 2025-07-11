using Domain.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<object> GetDoctorByIdAsync(int id)
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
            return await _context.Users.Where(d => d.UserName == name)
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





    }
}

