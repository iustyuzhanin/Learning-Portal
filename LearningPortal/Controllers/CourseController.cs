using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPortal.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список категорий
        /// </summary>
        public List<string> listCategory()
        {
            List<string> categories = new List<string>();

            foreach (var category in _context.Categories)
            {
                categories.Add(category.Name);
            }

            return categories;
        }

        public IActionResult Index()
        {
            var сourses = _context.Courses.ToList();
            return View(сourses);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(listCategory());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            ViewBag.Categories = new SelectList(listCategory());

            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Name = model.Name,
                    Category = model.Category,
                    Chapter = model.Chapter,
                    Description = model.Description,
                    Program = model.Program,
                    Image = model.Image,
                    //Teacher = model.Teacher,
                    //Students = model.Students
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index");
      
            }
            return View(model);
        }
    }
}
