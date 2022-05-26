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
using Microsoft.AspNetCore.Http;

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
            ///Required
            IsRequired(company);
            if (!ModelState.IsValid) return View();

            //Check
            PosterImageCheck(company);
            ImagesCheck(company);
            if (!ModelState.IsValid) return View();

            //Create
            CreatePosterImage(company);
            CreateImage(company);

            //SaveChange
            SaveChange(company);
            SaveContext();
            return RedirectToAction(nameof(Index));
        }

        // GET: Manage/Companies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!CompanyExists(id)) return RedirectToAction("notfound", "error");
            var company = await _context.Companies.Include(x => x.CompanyImages).FirstOrDefaultAsync(x => x.Id == id);
            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            if (!CompanyExists(company.Id)) return RedirectToAction("notfound", "error");

            Company companyExist = await _context.Companies.Include(x => x.CompanyImages).FirstOrDefaultAsync(x => x.Id == company.Id);
            //Required
            EditIsRequired(company);
            if (!ModelState.IsValid) return View(companyExist);

            if (company.PosterImageFile != null)
            {
                PosterImageCheck(company);
                if (!ModelState.IsValid) return View(companyExist);
                var posterFile = company.PosterImageFile;
                CompanyImages posterImage = companyExist.CompanyImages.FirstOrDefault(x => x.PosterStatus == true);

                var filename = FileSave(company);
                FileManager.Delete(_env.WebRootPath, "uploads/companies", posterFile.FileName);
                posterImage.Image = filename;
            }
            if (company.CompanyImagesIds != null)
            {
                foreach (var item in companyExist.CompanyImages.Where(x => x.PosterStatus == false && !company.CompanyImagesIds.Contains(x.Id)))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/companies", item.Image);
                }
                companyExist.CompanyImages.RemoveAll(x => x.PosterStatus == false && !company.CompanyImagesIds.Contains(x.Id));

            }
            else
            {
                foreach (var item in companyExist.CompanyImages.Where(x => x.PosterStatus == false))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/companies", item.Image);
                }
                companyExist.CompanyImages.RemoveAll(x => x.PosterStatus == false);
            }
            if (company.ImageFiles != null)
            {
                EditImageSave(company, companyExist);
            }
            EditChange(company, companyExist);
            SaveContext();
            return RedirectToAction(nameof(Index));

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
            if (company.Title == null)
            {
                ModelState.AddModelError("Title", "Title is required");

            }
            if (company.Website == null)
            {
                ModelState.AddModelError("Website", "Website is required");

            }
            if (company.Description == null)
            {
                ModelState.AddModelError("Description", "Description is required");

            }

            if (company.PosterImageFile == null)
            {
                ModelState.AddModelError("PosterImageFile", "PosterImageFile is required");

            }
            if (company.ImageFiles == null)
            {
                ModelState.AddModelError("ImageFiles", "ImageFiles is required");

            }
        }
        private void EditIsRequired(Company company)
        {
            if (company.Name == null)
            {
                ModelState.AddModelError("Name", "Name is required");
            }
            if (company.Title == null)
            {
                ModelState.AddModelError("Title", "Title is required");

            }
            if (company.Website == null)
            {
                ModelState.AddModelError("Website", "Website is required");

            }
            if (company.Description == null)
            {
                ModelState.AddModelError("Description", "Description is required");

            }
        }
        private void PosterImageCheck(Company company)
        {
            if (company.PosterImageFile.ContentType != "image/png" && company.PosterImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("PosterImageFile", "Image type only (png and jpeg");
            }
            if (company.PosterImageFile.Length > 2097152)
            {
                ModelState.AddModelError("PosterImageFile", "PosterImageFile max size is 2MB");
            }
        }
        private void ImagesCheck(Company company)
        {
            foreach (var image in company.ImageFiles)
            {
                if (image.ContentType != "image/png" && image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFiles", "PosterImageFile is required");
                    break;
                }
                if (image.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFiles", "PosterImageFile max size is 2MB");
                    break;
                }
            }
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
        private string FileSave(Company company)
        {
            string image = FileManager.Save(_env.WebRootPath, "uploads/companies", company.PosterImageFile);
            return image;
        }
        private void EditImageSave(Company company, Company companyExist)
        {
            foreach (var image in company.ImageFiles)
            {
                if (image.ContentType != "image/png" && image.ContentType != "image/jpeg")
                {
                    continue;
                }
                if (image.Length > 2097152)
                {
                    continue;
                }
                CompanyImages newImage = new CompanyImages
                {
                    PosterStatus = false,
                    Image = FileManager.Save(_env.WebRootPath, "uploads/companies", image),

                };
                if (companyExist.CompanyImages == null)
                {
                    companyExist.CompanyImages = new List<CompanyImages>();
                }
                companyExist.CompanyImages.Add(newImage);
            }

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

        private void EditChange(Company newCompany, Company oldCompany)
        {
            oldCompany.Name = newCompany.Name;
            oldCompany.Title = newCompany.Title;
            oldCompany.Description = newCompany.Description;
            oldCompany.IsHome = newCompany.IsHome;
            oldCompany.Website = newCompany.Website;
            oldCompany.ModifiedDate = DateTime.UtcNow.AddHours(4);
        }
        private void SaveContext()
        {
            _context.SaveChanges();
        }
        private void SaveChange(Company company)
        {
            _context.Add(company);
        }


        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        //        _context.Update(company);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CompanyExists(company.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
