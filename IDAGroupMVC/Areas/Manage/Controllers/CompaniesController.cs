using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDAGroupMVC.Models;
using IDAGroupMVC.Helper;
using Microsoft.AspNetCore.Hosting;

namespace IDAGroupMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles = "SuperAdmin , Admin")]
    public class CompaniesController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public CompaniesController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Manage/Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.Include(x => x.CompanyImages).ToListAsync());
        }


        // GET: Manage/Companies/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            IsRequired(company);
            if (!ModelState.IsValid) return View();
            var PosterIsCheck = PosterImageCheck(company);
            if (!PosterIsCheck) ModelState.AddModelError("", "");
           
            var isCheck = ImagesCheck(company);
            if (!isCheck) ModelState.AddModelError("", "");

          

            CreatePosterImage(company);
            CreateImage(company);

            _context.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Manage/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Manage/Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Manage/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
        private void IsRequired(Company company)
        {
            if (company.Name == null)
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            else if (company.Title == null)
            {
                ModelState.AddModelError("Title", "Title is required");

            }
            else if (company.Website == null)
            {
                ModelState.AddModelError("Website", "Website is required");

            }
            else if (company.Description == null)
            {
                ModelState.AddModelError("Description", "Description is required");

            }

            else if (company.PosterImageFile == null)
            {
                ModelState.AddModelError("PosterImageFile", "PosterImageFile is required");

            }
            else if (company.CompanyImages == null)
            {
                ModelState.AddModelError("CompanyImages", "CompanyImages is required");

            }
        }
        private bool PosterImageCheck(Company company)
        {
            if (company.PosterImageFile.ContentType != "image/png" && company.PosterImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("PosterImageFile", "Image type only (png and jpeg");
                return false;
            }
            if (company.PosterImageFile.Length > 2097152)
            {
                ModelState.AddModelError("PosterImageFile", "PosterImageFile max size is 2MB");
                return false;
            }
            return true;
        }
        private bool ImagesCheck(Company company)
        {
            foreach (var image in company.ImageFiles)
            {
                if (image.ContentType != "image/png" && image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterImageFile", "PosterImageFile is required");
                    return false;
                }
                if (image.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImageFile", "PosterImageFile max size is 2MB");
                    return false;
                }
            }

            return true;
        }

        private void CreatePosterImage(Company company)
        {
            CompanyImages Posterimage = new CompanyImages
            {
                PosterStatus = true,
                Companys = company,
                Image = FileManager.Save(_env.WebRootPath, "uploads/companies", company.PosterImageFile)
            };
            _context.CompanyImages.Add(Posterimage);
        }
        private void CreateImage(Company company)
        {
            CompanyImages image = new CompanyImages
            {
                PosterStatus = false,
                Companys = company,
                Image = FileManager.Save(_env.WebRootPath, "uploads/companies", company.PosterImageFile)
            };
            _context.CompanyImages.Add(image);
        }
    }
}
