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
            return View(productVM);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Company company = await _context.Companies.Include(x=>x.CompanyImages).Where(x => x.IsDelete == false).FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return RedirectToAction("notfound","error");
            }

            return View(company);
        }
    }
}
