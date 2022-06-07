using IDAGroupMVC.Helper;
using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly DataContext _context;

        public ErrorController(DataContext context)
        {
            _context = context;
        }
        public IActionResult NotFound()
        {
            ClickDateCounter.ClickCounter(_context, "ClientError",false);
            return View();
        }
    }
}
