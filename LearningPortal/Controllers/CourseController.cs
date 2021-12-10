using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPortal.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CourseController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Список категорий
        /// </summary>
        public List<Category> listCategory()
        {
            List<Category> categories = new List<Category>();

            foreach (var category in _context.Categories)
            {
                categories.Add(category);
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
                string wwwRootPath = _hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                model.ImageName = model.Name + DateTime.Now.ToString("yyMMdd-hms")+extension;
                string path = Path.Combine(wwwRootPath + "/images/", model.ImageName);

                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                Course course = new Course
                {
                    Name = model.Name,
                    Category = model.Category,
                    //Chapter = model.Chapter,
                    Description = model.Description,
                    Program = model.Program,
                    ImageName = model.ImageName,
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
