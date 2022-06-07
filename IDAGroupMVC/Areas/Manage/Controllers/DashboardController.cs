using IDAGroupMVC.Areas.Manage.ViewModels;
using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class DashboardController : Controller
    {
        private readonly DataContext _context;

        public DashboardController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var today = DateTime.UtcNow.AddHours(4).Day;
            var month = DateTime.UtcNow.AddHours(4).Month;

            DashboardViewModel dashVM = new DashboardViewModel
            {
                CompanyCount = _context.Companies.Count(),
                TodayComVisited = _context.ClickDates.Where(x => x.ViewCount.IsCompany == true && x.IsDelete == false && x.Date.Day == today).Count(),

                //Home
                HomeCountDay = _context.ClickDates.Where(x => x.ViewCountId == 6 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Day == today).Count(),
                HomeCountMonth = _context.ClickDates.Where(x => x.ViewCountId == 6 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Month == month).Count(),
                Home = _context.ViewCounts.FirstOrDefault(x=>x.Id== 6),

                //Products
                ProductsCountDay = _context.ClickDates.Where(x => x.ViewCountId == 4 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Day == today).Count(),
                ProductsCountMonth = _context.ClickDates.Where(x => x.ViewCountId == 4 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Month == month).Count(),
                Products = _context.ViewCounts.FirstOrDefault(x => x.Id == 4),

                //About
                AboutCountDay = _context.ClickDates.Where(x => x.ViewCountId == 7 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Day == today).Count(),
                AboutCountMonth = _context.ClickDates.Where(x => x.ViewCountId == 7 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Month == month).Count(),
                About = _context.ViewCounts.FirstOrDefault(x => x.Id == 7),
             
                //Contact
                ContactCountDay = _context.ClickDates.Where(x => x.ViewCountId == 5 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Day == today).Count(),
                ContactCountMonth = _context.ClickDates.Where(x => x.ViewCountId == 5 && x.ViewCount.IsCompany == false && x.IsDelete == false && x.Date.Month == month).Count(),
                Contact = _context.ViewCounts.FirstOrDefault(x => x.Id == 5),

            };
            return View(dashVM);
        }
    }
}
