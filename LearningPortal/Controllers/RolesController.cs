using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Models;
using Microsoft.AspNetCore.Identity;

namespace LearningPortal.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUserModel> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUserModel> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var listUsersRoles = new AppUserRoleModel
            {
                Users = _userManager.Users.ToList(),
                Roles = _roleManager.Roles.ToList()
            };
            return View(listUsersRoles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppRoleCreateModel model)
        {
            IdentityRole role = new IdentityRole
            {
                Name = model.RoleName
            };

            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Error", error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;

                IdentityResult resultEdit = await _roleManager.UpdateAsync(role);

                if (resultEdit.Succeeded)
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
    }
}
