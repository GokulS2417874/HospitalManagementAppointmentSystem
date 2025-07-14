using Domain.Data;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Domain.Models;

namespace HospitalManagementAndAppointmentSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAdminRepository _repo;
        public AdminController(AppDbContext context,IAdminRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        [HttpGet("EmployeesDetailsList")]
        public async Task<IActionResult> EmployeesList()
        {
            var EmployeeDetails = await _repo.ApprovedByAdmin();
            return Ok(EmployeeDetails);
        }
        [HttpPut("Admin")]
        public async Task<IActionResult> ApprovedByAdmin()
        {
            var EmployeeDetails = await _repo.ApprovedByAdmin();
            foreach(var emp in EmployeeDetails)
            {
                if(!emp.IsApprovedByAdmin)
                {
                    emp.IsApprovedByAdmin = true;
                    _context.SaveChanges();

                }
            }
            return Ok();
        }
    }
}
