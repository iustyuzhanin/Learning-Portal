using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LearningPortal.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories;
            return View(categories);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower())
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
                
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var category = await _context.Categories.FindAsync(model.Id);
                category.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower());

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
                    
            }
            return View(model);
        }

    }
}
