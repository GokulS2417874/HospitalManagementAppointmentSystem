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
            var EmployeeDetailsList = await _context.Users.Where(q => !q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.Pending)).ToListAsync();
            return EmployeeDetailsList;
        }
        public async Task<string?> RemoveNotApprovedEmployeeAsync(int employeeId)
        {
            if (employeeId == 0)
                return null;

            var employee = await _context.Users
                .FirstOrDefaultAsync(q => q.UserId == employeeId &&
                                          q.Role != UserRole.Patient &&
                                          q.IsApprovedByAdmin == AdminApproval.NotApproved);

            if (employee == null)
                return null;

            _context.Users.Remove(employee);
            await _context.SaveChangesAsync();

            return $"{employeeId} Employee removed successfully";
        }
        public async Task<string?> ApproveEmployeeRegistrationAsync(int employeeId, AdminApproval isApproved)
        {
            var employee = await _context.Users.FirstOrDefaultAsync(e => e.UserId == employeeId);

            if (employee == null)
                return null;

            employee.IsApprovedByAdmin = isApproved;
            await _context.SaveChangesAsync();

            return isApproved == AdminApproval.Approved
                ? $"{employee.UserId} is Approved by Admin"
                : $"{employee.UserId} is Not Approved by Admin";
        }


        public async Task<Users?> RetrieveEmpDetailsById(int EmployeeId)
        {
            var EmployeeDetails = await _context.Users.FirstOrDefaultAsync(q => q.UserId == EmployeeId && !q.Role.Equals(UserRole.Patient) && q.IsApprovedByAdmin.Equals(AdminApproval.Pending));

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
        public async Task<string?> AllocateShiftForEmployeeAsync(int employeeId, ShiftTime shift)
        {
            var employee = await _context.Users.FirstOrDefaultAsync(e => e.UserId == employeeId);

            if (employee == null)
                return null;

            employee.Shift = shift;
            await _context.SaveChangesAsync();

            return $"{employeeId} shift is allocated by admin";
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

