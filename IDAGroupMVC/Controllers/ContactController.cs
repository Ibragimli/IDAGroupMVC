using IDAGroupMVC.Models;
using IDAGroupMVC.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static IDAGroupMVC.Services.EmailServices;

namespace IDAGroupMVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public ContactController(DataContext context,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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
            bool resultEmail = EmailValidate(contact.Email);
            if (resultEmail == false)
            {
                ModelState.AddModelError(contact.Email, "-isn`t email");
                return View(contactVM);
            }
            bool resultPhoneNumber = PhoneNumberValidate(contact.PhoneNumber);
            if (resultPhoneNumber == false)
            {
                ModelState.AddModelError(contact.PhoneNumber, "-isn`t correct number");
                return View(contactVM);
            }
            if (!ModelState.IsValid)
            {
                return View(contactVM);
            }

            Contact newContact = CreateContact(contact);

            _context.Contacts.Add(newContact);
            _context.SaveChanges();
            _emailService.Send(contact.Email, "Contact", contact.Email);
            return RedirectToAction("contactus", "contact");
        }
        private static bool EmailValidate(string emailAddress)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //if email is valid
            if (Regex.IsMatch(emailAddress, pattern))
            {
                return true;
            }
            return false;
        }
        private static bool PhoneNumberValidate(string phoneNumber)
        {
            string pattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,7}$";

            //if phonennumber is valid
            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return true;
            }
            return false;
        }

        private static Contact CreateContact(Contact contact)
        {
            var newContact = new Contact
            {
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Fullname = contact.Fullname,
                Text = contact.Text,
                Subject = contact.Subject,
            };
            return newContact;
        }
    }
}
