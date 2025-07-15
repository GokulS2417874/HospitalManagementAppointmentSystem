using Domain.Data;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static Domain.Models.Enum;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAdminRepository _repo;
    public AdminController(AppDbContext context, IAdminRepository repo)
    {
        _context = context;
        _repo = repo;
    }
    [HttpGet("PendingEmployeesDetailsList")]
    public async Task<IActionResult> PendigEmployeesList()
    {
        var EmployeeDetails = await _repo.PendingApprovedByAdminList();
        return Ok(EmployeeDetails);
    }
        [HttpPut("RegistrationApproval")]
        public async Task<IActionResult> ApprovedByAdmin(int EmployeeId, AdminApproval IsApproved)
        {
            var employee = await _repo.RetrieveEmpDetailsById(EmployeeId);

            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            employee.IsApprovedByAdmin = IsApproved;
            await _context.SaveChangesAsync();

            if (IsApproved == AdminApproval.Approved)
            {
                return Ok($"{employee.UserId} is Approved by Admin");
            }
            else
            {
                return Ok($"{employee.UserId} is Not Approved by Admin");
            }
        }

    [HttpGet("NotApprovedList")]
    public async Task<IActionResult> NotApprovedEmployeesList()
    {
        var EmployeeDetails = await _repo.NotApprovedByAdminList();
        return Ok(EmployeeDetails);
    }
        [HttpDelete("Delete-Not-Approved")]
        public async Task<IActionResult> RemoveEmployee(int EmployeeId)
        {
            if (EmployeeId == 0)
            {
                return BadRequest($"{EmployeeId}Invalid Employee ID");
            }

            var employee = await _context.Users
                .FirstOrDefaultAsync(q => q.UserId == EmployeeId
                    && q.Role != UserRole.Patient
                    && q.IsApprovedByAdmin == AdminApproval.NotApproved);

            if (employee == null)
            {
                return NotFound($"{EmployeeId} Employee not found or already approved");
            }

            _context.Users.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok($"{EmployeeId}Employee removed successfully");

        }

        [HttpGet("ShiftNotAllocatedEmployeesList")]
    public async Task<IActionResult> ShiftNotAllocatedList()
    {
        var EmployeeDetails = await _repo.ShiftNotAllocatedList();
        return Ok(EmployeeDetails);
    }
    [HttpPut("Allocate-Shift")]
    public async Task<IActionResult> AllocateShifts(int EmployeeId, ShiftTime Shift)
    {
        var EmployeeDetails = await _repo.AllocateShiftforEmployees(EmployeeId, Shift);
            if (EmployeeDetails == null)
            {
                return BadRequest("EmployeeId not Found");
            }
            else
            {
                EmployeeDetails.Shift = Shift;
            }
        await _context.SaveChangesAsync();
        return Ok($"{EmployeeId} shift is Allocated by admin");
    }
    [HttpGet("EmployeesDetailsList")]
    public async Task<IActionResult> EmployeesList()
    {
        var EmployeeDetails = await _repo.EmployeeList();
        return Ok(EmployeeDetails);
    }
    [HttpGet("List/Appointments/Today")]
    public IActionResult GetTodayAppointments()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var list = _repo.GetAppointmentsByDate(today);
        return Ok(list);
    }

    [HttpGet("Count/Appointments/Today")]
    public IActionResult GetTodayAppointmentCount()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var count = _repo.GetAppointmentCountByDate(today);
        return Ok(new { Date = today.ToString("yyyy-MM-dd"), Count = count });
    }

    [HttpGet("List/Appointments/ByDate")]
    public IActionResult GetAppointmentsByDate(string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Invalid date format. Use yyyy-MM-dd");

        var list = _repo.GetAppointmentsByDate(parsedDate);
        return Ok(list);
    }

    [HttpGet("Count/Appointments/ByDate")]
    public IActionResult GetAppointmentCountByDate(string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Invalid date format. Use yyyy-MM-dd");

        var count = _repo.GetAppointmentCountByDate(parsedDate);
        return Ok(new { Date = parsedDate.ToString("yyyy-MM-dd"), Count = count });
    }

    [HttpGet("List/Appointments/ByMonth")]
    public IActionResult GetAppointmentsByMonth(int month, int year)
    {
        var list = _repo.GetAppointmentsByMonth(month, year);
        return Ok(list);
    }

    [HttpGet("Count/Appointments/ByMonth")]
    public IActionResult GetAppointmentCountByMonth(int month, int year)
    {
        var count = _repo.GetAppointmentCountByMonth(month, year);
        return Ok(new { Month = month, Year = year, Count = count });
    }

    [HttpGet("List/Appointments/ByYear")]
    public IActionResult GetAppointmentsByYear(int year)
    {
        var list = _repo.GetAppointmentsByYear(year);
        return Ok(list);
    }

    [HttpGet("Count/Appointments/ByYear")]
    public IActionResult GetAppointmentCountByYear(int year)
    {
        var count = _repo.GetAppointmentCountByYear(year);
        return Ok(new { Year = year, Count = count });
    }
    }


}

