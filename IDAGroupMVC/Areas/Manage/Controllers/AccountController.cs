using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AppUser admin)
        {
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
    }
}
