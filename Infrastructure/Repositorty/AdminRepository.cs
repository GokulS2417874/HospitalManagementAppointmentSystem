using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interface;
using Domain.Models;
using Domain.Data;
using static Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorty
{
   
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Users?>> PendingApprovedByAdminList()
        {
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient)&&q.IsApprovedByAdmin.Equals(AdminApproval.Pending)).ToListAsync();
            return EmployeeDetailsList;
        }
        public async Task<Users?> RetrieveEmpDetailsById(int EmployeeId)
        {
            var EmployeeDetails = await _context.Users.FirstOrDefaultAsync(q => q.UserId==EmployeeId&&!q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.Pending));
            return EmployeeDetails;
        }
        public async Task<List<Users?>> NotApprovedByAdminList()
        {
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.NotApproved)).ToListAsync();
            return EmployeeDetailsList;
        }
        //public async Task<Users> RemoveEmployee(int EmployeeId)
        //{

        //    var EmployeeDetails = await _context.Users.FirstAsync(q => q.UserId == EmployeeId && !q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.NotApproved));
        //   // Console.WriteLine($"Found Employee: {EmployeeDetails?.UserId}, Role: {EmployeeDetails?.Role}, Approved: {EmployeeDetails?.IsApprovedByAdmin}");

        //    if (EmployeeDetails != null)
        //    {
        //        _context.Users.Remove(EmployeeDetails);
        //        await _context.SaveChangesAsync();
               
        //    }
        //    return EmployeeDetails;


        //}
        public async Task<List<Users?>> ShiftNotAllocatedList()
        {
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient) && q.Shift.Equals(ShiftTime.NotAllocated)).ToListAsync();
            return EmployeeDetailsList;
        }
        public async Task<Users?> AllocateShiftforEmployees(int EmployeeId,ShiftTime shift)
        {
            var EmployeeDetails = await _context.Users.FirstOrDefaultAsync(q => !q.Role.Equals(UserRole.Patient) && q.UserId == EmployeeId&& q.Shift.Equals(ShiftTime.NotAllocated));
            if (EmployeeDetails == null)
                return null;
            return EmployeeDetails;         
        }
        public async Task<List<Users?>> EmployeeList()
        {
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.Approved)).ToListAsync();
            return EmployeeDetailsList;
        }
        public List<Appointment> GetAppointmentsByDate(DateOnly date)
            {
                return _context.Appointments
                               .Where(a => a.AppointmentDate == date)
                               .ToList();
            }
        public int GetAppointmentCountByDate(DateOnly date)
            {
                return _context.Appointments
                               .Count(a => a.AppointmentDate == date);
            }
        public List<Appointment> GetAppointmentsByMonth(int month, int year)
            {
                return _context.Appointments
                               .Where(a => a.AppointmentDate.Month == month &&
                                           a.AppointmentDate.Year == year)
                               .ToList();
            }
        public int GetAppointmentCountByMonth(int month, int year)
            {
                return _context.Appointments
                               .Count(a => a.AppointmentDate.Month == month &&
                                           a.AppointmentDate.Year == year);
            }
        public List<Appointment> GetAppointmentsByYear(int year)
            {
                return _context.Appointments
                               .Where(a => a.AppointmentDate.Year == year)
                               .ToList();
            }
        public int GetAppointmentCountByYear(int year)
            {
                return _context.Appointments
                               .Count(a => a.AppointmentDate.Year == year);
            }
        }

    }

