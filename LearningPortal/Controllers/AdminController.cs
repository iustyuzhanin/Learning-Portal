using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Domains;
using LearningPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Список ролей
        /// </summary>
        public List<IdentityRole> listRole()
        {
            List<IdentityRole> positions = new List<IdentityRole>();

            foreach (var role in _roleManager.Roles)
            {
                positions.Add(role);
            }

            return positions;
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
            return View();
        }

        public IActionResult Courses()
        {
            var сourses = _context.Courses.ToList();
            return View(сourses);
        }

        public async Task<IActionResult> CourseEdit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> CourseEdit(Course model)
        {
            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(model.Id);

                //string wwwRootPath = _hostEnvironment.WebRootPath;
                ////string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                //string extension = Path.GetExtension(model.ImageFile.FileName);
                //model.ImageName = model.Name + DateTime.Now.ToString("yyMMdd-hms") + extension;
                //string path = Path.Combine(wwwRootPath + "/images/", model.ImageName);

                //using (var fileStream = new FileStream(path, FileMode.Create))
                //{
                //    await model.ImageFile.CopyToAsync(fileStream);
                //}

                course.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower());
                //course.Category = null;
                //course.Chapter = null;
                course.Description = model.Description;
                course.Program = model.Program;
                course.ImageName = model.ImageName;
                //course.Teacher = null;
                //course.Students = null;
                
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                return RedirectToAction("Courses","Admin");

            }
            return View(model);
        }

        public async Task<IActionResult> CourseDelete(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course != null)
            {
                _context.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Courses","Admin");
        }

        public IActionResult CourseCreate()
        {
            ViewBag.Categories = new SelectList(listCategory());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourseCreate(Course model)
        {
            ViewBag.Categories = new SelectList(listCategory());

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                model.ImageName = model.Name + DateTime.Now.ToString("yyMMdd-hms") + extension;
                string path = Path.Combine(wwwRootPath + "/images/", model.ImageName);

                using (var fileStream = new FileStream(path, FileMode.Create))
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
                return RedirectToAction("Courses","Admin");

            }
            return View(model);
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Students()
        {
            var students = _userManager.Users.Where(student => student.Role == "Student");
            return View(students);
        }

        public IActionResult Teachers()
        {
            var teachers = _userManager.Users.Where(teacher => teacher.Role == "Teacher");
            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = new SelectList(listRole());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateModel model)
        {
            ViewBag.Positions = new SelectList(listRole());
            if (ModelState.IsValid)
            {
                AppUserModel user = new AppUserModel()
                {
                    UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower()),
                    Email = model.Email.ToLower(),
                    Role = model.Role,
                    FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FullName.ToLower())
                };

                IdentityResult resultCreate = await _userManager.CreateAsync(user, model.Password);
                //IdentityResult resultAddToRole = await _userManager.AddToRoleAsync(user, model.Role);
                if (resultCreate.Succeeded /*&& resultAddToRole.Succeeded*/)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    foreach (var error in resultCreate.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    //foreach (var error in resultAddToRole.Errors)
                    //{
                    //    ModelState.AddModelError("", error.Description);
                    //}
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.Positions = new SelectList(listRole());

            AppUserEditModel userEdit = new AppUserEditModel()
            {
                Name = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                //Role = user.Role
            };
            
            return View(userEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUserEditModel model)
        {
            ViewBag.Positions = new SelectList(listRole());
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                user.UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower());
                user.Email = model.Email.ToLower();
                //user.Role = model.Role;
                user.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FullName.ToLower());

                IdentityResult resultEdit = await _userManager.UpdateAsync(user);
                //IdentityResult resultAddToRole = await _userManager.AddToRoleAsync(user, model.Role);
                await _context.SaveChangesAsync();

                if (resultEdit.Succeeded /*&& resultAddToRole.Succeeded*/)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    foreach (var error in resultEdit.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            AppUserModel user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users","Admin");
        }

        public async Task<IActionResult> Profile(string name)
        {
            AppUserModel user = await _userManager.FindByNameAsync(name);

            return View(user);
        }


    }
}
