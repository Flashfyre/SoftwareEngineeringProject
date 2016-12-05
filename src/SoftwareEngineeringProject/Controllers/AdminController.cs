using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Admin[/Index]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.Name == null) // If the user is not logged in, redirect them to the login page
                return Redirect("/Account/Login");
            else
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                if (!await _userManager.IsInRoleAsync(user, "Administrator")) // If the user is not an administrator, redirect them to the homepage
                    return Redirect("/");
            }
            return View();
        }

        // GET: /Admin/AdminGrid
        [HttpGet]
        public async Task<IActionResult> AdminGrid()
        {
            if (HttpContext.User.Identity.Name == null) // If the user is not logged in, redirect them to the login page
                return Redirect("/Account/Login");
            else
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (!await _userManager.IsInRoleAsync(user, "Administrator")) // If the user is not ah administrator, redirect them to the homepage
                    return Redirect("/");
            }

            return PartialView("MvcGrid/_AdminGrid", _userManager.Users.Include(u => u.Roles).AsEnumerable());
        }

        [HttpPost]
        public async Task<JsonResult> LockAccount(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            bool success = false;
            string message = null;

            if (user != null)
            {
                if (!await _userManager.IsInRoleAsync(user, "Administrator"))
                {
                    if (user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
                    {
                        success = true;
                        user.LockoutEnd = new DateTime(9999, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        await _userManager.UpdateAsync(user);
                    }
                    else
                        message = "Error: The user account with email '" + email + "' is already locked!";
                }
                else
                    message = "Error: An administrator account may not be locked!";
            }
            else
                message = "Error: No user exists with the email '" + email + "'!";

            return Json(new object[] { success, message });
        }

        [HttpPost]
        public async Task<JsonResult> UnlockAccount(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            bool success = false;
            string message = null;

            if (user != null)
            {
                if (user.LockoutEnd != null)
                {
                    success = true;
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                }
                else
                    message = "Error: The user account with email '" + email + "' is already unlocked!";
            }
            else
                message = "Error: No user exists with the email '" + email + "'!";

            return Json(new object[] { success, message });
        }
    }
}
