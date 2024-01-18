using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpicyTemplate.Areas.SpicyAdmin.Controllers
{
    [Area("SpicyAdmin")]
    //[Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
