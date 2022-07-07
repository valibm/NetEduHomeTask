using Business.ViewModels;
using DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Utilities.Helpers.Enums;

namespace NetEduHomeTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, 
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet(nameof(Register))]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterVM registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            AppUser appUser = new AppUser();

            appUser.FirstName = registerVm.Firstname;
            appUser.LastName = registerVm.Lastname;
            appUser.Email = registerVm.Email;
            appUser.UserName = registerVm.Username;

            var result = await _userManager.CreateAsync(appUser, registerVm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVm);
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());

            if (!roleResult.Succeeded)
            {
                foreach (var item in roleResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVm);
            }

            return RedirectToAction("Index", controllerName: "Home");
        }

        [HttpGet(nameof(Login))]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            AppUser appUser = await _userManager.FindByNameAsync(loginVM.Username);

            if (appUser is null)
            {
                ModelState.AddModelError("", "Username is wrong");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.RememberMe, true);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Please confirm your account");
                return View(loginVM);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your profile is locked out.\nPlease try again later.");
                return View(loginVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid password");
                return View(loginVM);
            }

            if (await _userManager.IsInRoleAsync(appUser, Roles.Admin.ToString()))
            {
                return RedirectToAction("Index", controllerName: "Dashboard", new {Areas = "Admin"});
            }

            return RedirectToAction("Index", controllerName: "Home");
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", controllerName: "Home");
        }

        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!(await _roleManager.RoleExistsAsync(role.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
        }
    }
}
