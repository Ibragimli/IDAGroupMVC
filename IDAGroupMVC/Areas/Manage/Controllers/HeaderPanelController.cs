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
    public class HeaderPanelController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public HeaderPanelController(DataContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public IActionResult Index()
        {
            var header = _context.Settings.AsQueryable();
            header = header.Where(x => x.Key.StartsWith("Head"));
            return View(header.ToList());
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!SettingManager.SettingExists(id, _context)) return RedirectToAction("notfound", "error");
            var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Setting setting)
        {
            if (!SettingManager.SettingExists(setting.Id, _context)) return RedirectToAction("notfound", "error");

            Setting settingExist = await _context.Settings.FirstOrDefaultAsync(x => x.Id == setting.Id);

            //Required
            IsRequired(setting);
            if (!ModelState.IsValid) return View(settingExist);

            if (setting.KeyImageFile != null)
            {
                PosterImageCheck(setting);
                if (!ModelState.IsValid) return View(settingExist);
                SettingManager.EditPosterImageSave(setting, settingExist, _env, "header");
            }
            if (setting.Value != null) SettingManager.EditChange(setting, settingExist);

            SettingManager.SaveChange(_context);
            return RedirectToAction(nameof(Index));

        }

        private void IsRequired(Setting setting)
        {
            if (setting.Value == null && setting.KeyImageFile == null) ModelState.AddModelError("KeyImageFile", "KeyImageFile is required");
        }

        private void PosterImageCheck(Setting setting)
        {
            if (setting.KeyImageFile.ContentType != "image/png" && setting.KeyImageFile.ContentType != "image/jpeg") ModelState.AddModelError("KeyImageFile", "Image type only (png and jpeg");
            if (setting.KeyImageFile.Length > 2097152) ModelState.AddModelError("KeyImageFile", "Image max size is 2MB");
        }
    }
}
