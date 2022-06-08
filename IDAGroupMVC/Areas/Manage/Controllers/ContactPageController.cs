using IDAGroupMVC.Helper;
using IDAGroupMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class ContactPageController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactPageController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Manage/Setting
        public async Task<IActionResult> Index()
        {
            var contact = _context.Settings.AsQueryable();
            contact = contact.Where(x => x.Key.StartsWith("Contact"));
            return View(contact.ToList());
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!SettingExists(id)) return RedirectToAction("notfound", "error");
            var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            return View(setting);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting setting)
            {
            if (!SettingExists(setting.Id)) return RedirectToAction("notfound", "error");

            Setting settingExist = await _context.Settings.FirstOrDefaultAsync(x => x.Id == setting.Id);

            //Required
            IsRequired(setting);
            if (!ModelState.IsValid) return View(settingExist);

            if (setting.KeyImageFile != null)
            {
                PosterImageCheck(setting);
                if (!ModelState.IsValid) return View(settingExist);
                EditPosterImageSave(setting, settingExist);
            }
            if (setting.Value != null) EditChange(setting, settingExist);

            SaveChange();
            return RedirectToAction(nameof(Index));

        }



        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }

        private void IsRequired(Setting setting)
        {
            if (setting.Value == null && setting.KeyImageFile == null)
            {
                ModelState.AddModelError("", "Value is required");
            }
        }
        private void EditChange(Setting setting, Setting settingExist)
        {
            settingExist.Value = setting.Value;
            settingExist.ModifiedDate = DateTime.UtcNow.AddHours(4);
        }
        private void SaveChange()
        {
            _context.SaveChanges();
        }
        private void AddContext(Setting setting)
        {
            _context.Add(setting);

        }
        private string FileSave(Setting setting)
        {
            string image = FileManager.Save(_env.WebRootPath, "uploads/settings", setting.KeyImageFile);
            return image;
        }

        private void PosterImageCheck(Setting setting)
        {
            if (setting.KeyImageFile.ContentType != "image/png" && setting.KeyImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("PosterImageFile", "Image type only (png and jpeg");
            }
            if (setting.KeyImageFile.Length > 2097152)
            {
                ModelState.AddModelError("PosterImageFile", "PosterImageFile max size is 2MB");
            }
        }
        private void EditPosterImageSave(Setting setting, Setting settingExist)
        {
            var posterFile = setting.KeyImageFile;

            var filename = FileSave(setting);
            FileManager.Delete(_env.WebRootPath, "uploads/settings", posterFile.FileName);
            settingExist.ValueImage = filename;
        }


    }
}