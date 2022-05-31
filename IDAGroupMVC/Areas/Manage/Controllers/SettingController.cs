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
    public class SettingController : Controller
    {
        private readonly DataContext _context;

        public SettingController(DataContext context)
        {
            _context = context;
        }

        // GET: Manage/Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }


        // GET: Manage/Companies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!SettingExists(id)) return RedirectToAction("notfound", "error");
            var company = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            return View(company);
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


            EditChange(setting, settingExist);
            SaveChange();
            return RedirectToAction(nameof(Index));

        }

        // GET: Manage/Companies/Delete/5


        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }

        private void IsRequired(Setting setting)
        {
            if (setting.Value == null)
            {
                ModelState.AddModelError("Value", "Value is required");
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
        private void AddContext(Setting company)
        {
            _context.Add(company);
        }

    }
}
