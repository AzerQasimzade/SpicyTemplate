using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.Areas.SpicyAdmin.ViewModels.Employee;
using SpicyTemplate.Areas.SpicyAdmin.ViewModels.Setting;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;

namespace SpicyTemplate.Areas.SpicyAdmin.Controllers
{
    [Area("SpicyAdmin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync(); 
            return View(settings);
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Setting existed = await _context.Settings.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null)
            {
                return NotFound();
            }
            UpdateSettingVM settingVM = new UpdateSettingVM
            {
               Key= existed.Key,
               Value= existed.Value,
            };
            return View(settingVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateSettingVM settingVM)
        {
            if (!ModelState.IsValid)
            {
                return View(settingVM);
            }
            Setting existed = await _context.Settings.FirstOrDefaultAsync(c => c.Id == id);
            if (existed == null)
            {
                return NotFound();
            }
            existed.Value= settingVM.Value;
            existed.Key=settingVM.Key;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");

        }
    }
}
