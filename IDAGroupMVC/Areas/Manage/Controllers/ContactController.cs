using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Contacts.ToList());
        }
        public IActionResult Detail(int id)
        {
            if (!_isExist(id)) return RedirectToAction("notfound", "error");
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            contact.IsRead = true;
            _context.SaveChanges();
            return View(contact);
        }
        private bool _isExist(int id)
        {
            return _context.Contacts.Any(x => x.Id == id);
        }
    }
}
