using IDAGroupMVC.Helper;
using IDAGroupMVC.Models;
using IDAGroupMVC.ViewModels.About;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly DataContext _context;

        public AboutController(DataContext context)
        {
            _context = context;
        }
        public IActionResult About()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                Settings = _context.Settings.Where(x => x.IsDelete == false).ToList(),
            };
            ClickDateCounter.ClickCounter(_context, "About",false);

            return View(aboutVM);
        }

    }
}
