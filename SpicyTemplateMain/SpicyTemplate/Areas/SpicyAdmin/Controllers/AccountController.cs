using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpicyTemplate.Areas.SpicyAdmin.ViewModels.Account;
using SpicyTemplate.Models;
using SpicyTemplate.Utilities.Enums;

namespace SpicyTemplate.Areas.SpicyAdmin.Controllers
{
    [Area("SpicyAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser
            {
                UserName=registerVM.Username,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email
            };
            IdentityResult identityResult=await _userManager.CreateAsync(user,registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home",new {area=""});
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser existedUser = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (existedUser is null)
            {
                existedUser = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                if (existedUser is null)
                {
                    ModelState.AddModelError(String.Empty, "UserName or Email or Password is incorrect");
                    return View();
                }
            }
            var passwordResult = await _signInManager.PasswordSignInAsync(existedUser, loginVM.Password, loginVM.IsRemembered, true);
            if (!passwordResult.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "UserName or Email or Password is incorrect");
                return View();
            }
            if (passwordResult.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Bloklandiniz hahah");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }     
        //public async Task<IActionResult> CreateRoles()
        //{
        //    foreach (var item in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole
        //        {
        //            Name = item.ToString(),
        //        });
        //    }
        //    return RedirectToAction("Index","Home");
        //}
    }
}
