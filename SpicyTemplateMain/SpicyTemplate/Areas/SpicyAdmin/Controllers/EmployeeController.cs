using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.Areas.SpicyAdmin.ViewModels.Employee;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;
using SpicyTemplate.Utilities.Enums;
using SpicyTemplate.Utilities.Extensions;

namespace SpicyTemplate.Areas.SpicyAdmin.Controllers
{
    [Area("SpicyAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!employeeVM.Photo.ValidateFileType(FileHelper.Image))
            {
                ModelState.AddModelError("Photo", "File type is not matching");
                return View();
            }
            if (!employeeVM.Photo.ValidateSizeType(SizeHelper.mb))
            {
                ModelState.AddModelError("Photo", "File size is not matching");
                return View();
            }
            string filename = Guid.NewGuid().ToString() + employeeVM.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets", "img",filename);
            FileStream file = new FileStream(path, FileMode.Create);
            await employeeVM.Photo.CopyToAsync(file);
            Employee employee = new Employee
            {
                Image=filename,
                Name = employeeVM.Name,
                Profession = employeeVM.Profession
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Employee employee = await _context.Employees.FirstOrDefaultAsync(c=>c.Id==id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Image.DeleteFile(_env.WebRootPath, "assets", "img");
             _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Employee existed = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null)
            {
                return NotFound();
            }
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Image = existed.Image,
                Name = existed.Name,
                Profession = existed.Profession  
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            Employee existed= await _context.Employees.FirstOrDefaultAsync(c=>c.Id==id);
            if (employeeVM.Photo is not null)
            {
                if (!employeeVM.Photo.ValidateFileType(FileHelper.Image))
                {
                    ModelState.AddModelError("Photo", "File type is not matching");
                    return View();
                }
                if (!employeeVM.Photo.ValidateSizeType(SizeHelper.mb))
                {
                    ModelState.AddModelError("Photo", "File size is not matching");
                    return View();
                }
                string filename = Guid.NewGuid().ToString() + employeeVM.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "img", filename);
                FileStream file = new FileStream(path, FileMode.Create);
                await employeeVM.Photo.CopyToAsync(file);
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img");
                existed.Image = filename;
            }
            existed.Profession=employeeVM.Profession;
            existed.Name = employeeVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
