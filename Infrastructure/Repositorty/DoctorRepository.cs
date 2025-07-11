using Domain.Data;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorty
{
 

    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetAllDoctorsAsync() => await _context.Users.Select(d => new
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
            return await _context.Users.Where(d => d.UserId == id).Select(d => new {
                d.UserId, d.UserName, d.Email, d.PhoneNumber,
                d.Specialization, d.Qualification, d.ExperienceYears,
                d.Consultant_fees, d.Active_Status,
                d.EmergencyContactName, d.EmergencyContactPhoneNumber,
                d.EmergencyContactRelationship
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> GetDoctorsByNameAsync(string name)
        {
            return await _context.Users.Where(d => d.UserName == name)
                .Select(d => new { d.UserName }).ToListAsync();
        }

        //public async Task<IEnumerable<object>> GetDoctorsBySpecializationAsync(string specialization)
        //{
        //   // return await _context.Users.Where(d => d.Specialization == specialization)
        //        .Select(d => new { d.UserName }).ToListAsync();
        //}

        
       
        }
    }

