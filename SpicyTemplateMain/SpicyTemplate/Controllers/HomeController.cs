using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;
using System.Diagnostics;

namespace SpicyTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

    }
}