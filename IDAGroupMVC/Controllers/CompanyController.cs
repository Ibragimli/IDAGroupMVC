using IDAGroupMVC.Helper;
using IDAGroupMVC.Models;
using IDAGroupMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDAGroupMVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Products(int page = 1)
        {


            var companys = _context.Companies.Include(x => x.CompanyImages).Where(x => x.IsDelete == false).AsQueryable();
            ProductViewModel productVM = new ProductViewModel
            {
                PagenatedCompanys = PagenetedList<Company>.Create(companys, page, 8)
            };
            ClickDateCounter.ClickCounter(_context, "Products",true);
            return View(productVM);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Company company = await _context.Companies.Include(x => x.CompanyImages).Include(x => x.ViewCount).ThenInclude(x => x.ClickDates).Where(x => x.IsDelete == false).FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return RedirectToAction("notfound", "error");
            }

            var viewCount = await _context.ViewCounts.FirstOrDefaultAsync(x => x.ClickName == company.Name);
            if (viewCount != null && company.ViewCountId == null)
            {
                company.ViewCountId = viewCount.Id;
                _context.SaveChanges();
            }
            if (company.ViewCountId != null)
            {
                viewCount.Count++;
                ClickDate date = new ClickDate
                {
                    ViewCountId = viewCount.Id,
                    Date = DateTime.UtcNow.AddHours(4),
                };
                _context.ClickDates.Add(date);
                _context.SaveChanges();
            }

            return View(company);
        }
    }
}
