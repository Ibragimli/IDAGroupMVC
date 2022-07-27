using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class EmailSettingController : Controller
    {
        private readonly DataContext _context;

        public EmailSettingController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var isEmail = _context.EmailSetting.FirstOrDefault(x => x.Id == 1);
            if (isEmail == null) return RedirectToAction("notfound", "error");
            return View(isEmail);
        }

        [HttpPost]
        public IActionResult Edit(EmailSetting email)
        {
            var isEmail = _context.EmailSetting.FirstOrDefault(x => x.Id == email.Id);
            if (isEmail == null) return RedirectToAction("notfound", "error");
            if (!ModelState.IsValid) return View(isEmail);

            isEmail.SmtpEmail = email.SmtpEmail;
            isEmail.SmtpHost = email.SmtpHost;
            isEmail.SmtpPort = email.SmtpPort;
            isEmail.SmtpPassword = email.SmtpPassword;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
