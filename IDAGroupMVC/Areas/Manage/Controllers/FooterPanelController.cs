using IDAGroupMVC.Helper;
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

    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin , Admin")]
    public class FooterPanelController : Controller
    {
        private readonly DataContext _context;

        public FooterPanelController(DataContext context) { _context = context; }

        public IActionResult Index()
        {
            var context = _context.Settings.Where(x => x.Key.EndsWith("Foot")).AsQueryable();

            return View(context.ToList());
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

            SettingManager.EditChange(setting, settingExist);
            SettingManager.SaveChange(_context);
            return RedirectToAction(nameof(Index));
        }
        private void IsRequired(Setting setting) { if (setting.Value == null) ModelState.AddModelError("Value", "Value is required"); }
    }
}
