using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;

namespace LearningPortal.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var сourses = _context.Courses.ToList();
            return View(сourses);
        }
    }
}
