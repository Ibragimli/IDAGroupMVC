using IDAGroupMVC.Models;
using IDAGroupMVC.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IDAGroupMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }
        public IActionResult ContactUs()
        {
            ContactViewModel contactVM = new ContactViewModel
            {
                Setting = _context.Settings.Where(x => x.IsDelete == false).ToList(),
                Contact = new Contact(),
            };
            return View(contactVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (contact == null)
            {
                return RedirectToAction("error", "notfound");
            }

            ContactViewModel contactVM = new ContactViewModel
            {
                Setting = _context.Settings.Where(x => x.IsDelete == false).ToList(),
                Contact = new Contact(),
            };
            bool result = Validate(contact.Email);
            if (result == false)
            {
                ModelState.AddModelError(contact.Email, "-isn`t email");
                return View(contactVM);
            }
            if (!ModelState.IsValid)
            {
                return View(contactVM);
            }
           
            return Ok(contact);
        }
        private static bool Validate(string emailAddress)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //if email is valid
            if (Regex.IsMatch(emailAddress, pattern))
            {
                return true;
            }
            return false;
        }
    }
}
