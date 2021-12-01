using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.DataAccessLayer;
using LearningPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace LearningPortal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
        private ApplicationDbContext _context;

        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Personal(string name)
        {
            AppUserModel user = await _userManager.FindByNameAsync(name);

            return View(user);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AppUserCreateModel model)
        {
            //ViewBag.Positions = new SelectList(listRole());
            if (ModelState.IsValid)
            {
                AppUserModel user = new AppUserModel()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    //Role = model.Role,
                    FullName = model.FullName
                };

                IdentityResult resultCreate = await _userManager.CreateAsync(user, model.Password);
                //IdentityResult resultAddToRole = await _userManager.AddToRoleAsync(user, model.Role);
                if (resultCreate.Succeeded /*&& resultAddToRole.Succeeded*/)
                {
                    return RedirectToAction("Index", "Home");
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

            //ViewBag.Positions = new SelectList(listRole());

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
            //ViewBag.Positions = new SelectList(listRole());
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                user.UserName = model.Name;
                user.Email = model.Email;
                //user.Role = model.Role;
                user.FullName = model.FullName;

                IdentityResult resultEdit = await _userManager.UpdateAsync(user);
                //IdentityResult resultAddToRole = await _userManager.AddToRoleAsync(user, model.Role);
                await _context.SaveChangesAsync();

                if (resultEdit.Succeeded /*&& resultAddToRole.Succeeded*/)
                {
                    return RedirectToAction("Personal","Account", new { name = model.Name });
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                AppUserModel user = await _userManager.FindByNameAsync(model.Login);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("Error", "Invalid user login or password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
