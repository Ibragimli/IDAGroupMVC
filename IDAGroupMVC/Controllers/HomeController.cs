using IDAGroupMVC.Helper;
using IDAGroupMVC.Models;
using IDAGroupMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                CompanySlider = _context.Companies.Include(x=>x.CompanyImages).Where(x=>x.IsHome==true).ToList(),
            };
            ClickDateCounter.ClickCounter(_context, "Home",false);

            return View(homeVM);
        }

        
    }
}
