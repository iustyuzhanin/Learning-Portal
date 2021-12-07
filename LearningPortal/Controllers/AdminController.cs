using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPortal.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public AdminController(UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
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
                    //Role = model.Role,
                    FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.FullName.ToLower())
                };

                IdentityResult resultCreate = await _userManager.CreateAsync(user, model.Password);
                //IdentityResult resultAddToRole = await _userManager.AddToRoleAsync(user, model.Role);
                if (resultCreate.Succeeded /*&& resultAddToRole.Succeeded*/)
                {
                    return RedirectToAction("Index");
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
                    return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile(string name)
        {
            AppUserModel user = await _userManager.FindByNameAsync(name);

            return View(user);
        }


    }
}
