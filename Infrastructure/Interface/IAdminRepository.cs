using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Domain.Models.Enum;

namespace Infrastructure.Interface
{
    public interface IAdminRepository
    {
        Task<List<Users>> PendingApprovedByAdminList();
        Task<Users?> RetrieveEmpDetailsById(int EmployeeId);
        Task<List<Users>> NotApprovedByAdminList();
        // Task<Users> RemoveEmployee(int EmployeeId);
        Task<string?> ApproveEmployeeRegistrationAsync(int employeeId, AdminApproval isApproved);
        Task<string?> RemoveNotApprovedEmployeeAsync(int employeeId);
        Task<List<Users?>> ShiftNotAllocatedList();
        Task<string?> AllocateShiftForEmployeeAsync(int employeeId, ShiftTime shift);

        Task<List<Users>> EmployeeList();
        List<Appointment> GetAppointmentsByDate(DateOnly date);
        int GetAppointmentCountByDate(DateOnly date);
        List<Appointment> GetAppointmentsByMonth(int month, int year);
        int GetAppointmentCountByMonth(int month, int year);
        List<Appointment> GetAppointmentsByYear(int year);
        int GetAppointmentCountByYear(int year);
        


    }
}
