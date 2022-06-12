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

namespace IDAGroupMVC.wwwroot.uploads.settings.about
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class AboutPageController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutPageController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Manage/Setting
        public async Task<IActionResult> Index()
        {
            var about = _context.Settings.AsQueryable();
            about = about.Where(x => x.Key.StartsWith("About"));
            return View(about.ToList());
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!SettingManager.SettingExists(id, _context)) return RedirectToAction("notfound", "error");
            var about = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            return View(about);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting about)
        {
            if (!SettingManager.SettingExists(about.Id, _context)) return RedirectToAction("notfound", "error");

            Setting aboutExist = await _context.Settings.FirstOrDefaultAsync(x => x.Id == about.Id);

            //Required
            IsRequired(about);
            if (!ModelState.IsValid) return View(aboutExist);

            if (about.KeyImageFile != null)
            {
                PosterImageCheck(about);
                if (!ModelState.IsValid) return View(aboutExist);
                SettingManager.EditPosterImageSave(about, aboutExist, _env, "about");
            }
            if (about.Value != null) SettingManager.EditChange(about, aboutExist);

            SettingManager.SaveChange(_context);
            return RedirectToAction(nameof(Index));

        }

        private void IsRequired(Setting about)
        {
            if (about.Value == null && about.KeyImageFile == null) ModelState.AddModelError("KeyImageFile", "KeyImageFile is required");
        }

        private void PosterImageCheck(Setting about)
        {
            if (about.KeyImageFile.ContentType != "image/png" && about.KeyImageFile.ContentType != "image/jpeg") ModelState.AddModelError("KeyImageFile", "Image type only (png and jpeg");
            if (about.KeyImageFile.Length > 2097152) ModelState.AddModelError("KeyImageFile", "Image max size is 2MB");
        }
    }
}