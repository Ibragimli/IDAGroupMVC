using IDAGroupMVC.Areas.Manage.ViewModels;
using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Login()
        {
            AppUser admin = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (admin != null && admin.IsAdmin == true)
            {
                return RedirectToAction("index", "dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel adminVM)
        {
            AppUser adminExist = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == adminVM.Login);
            if (adminExist != null && adminExist.IsAdmin == true)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(adminExist, adminVM.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Username or Passoword is incorrect!");
                    return View();
                }
                return RedirectToAction("index", "dashboard");
            }
            ModelState.AddModelError("", "Password or Username incorrect! ");
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Logout(AppUser admin)
        {
            if (IsExist(admin)) return RedirectToAction("notfound", "error");
            _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        private bool IsExist(AppUser admin)
        {
            return _context.Users.Any(x => x.Id == admin.Id);
        }
        //public async Task<IActionResult> CreateRoleUser()
        //{
        //    //var role1 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    //var role2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    //var role3 = await _roleManager.CreateAsync(new IdentityRole("User"));

        //    AppUser SuperAdmin = new AppUser { FullName = "Super Admin", UserName = "SuperAdmin" };
        //    var admin = await _userManager.CreateAsync(SuperAdmin, "Admin123");
        //    var resultRole = await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");

        //    return Ok(resultRole);
        //}
    }
}
