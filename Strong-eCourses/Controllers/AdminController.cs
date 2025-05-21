using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strong_eCourses.Models;

namespace Strong_eCourses.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public AdminController(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
    {

        _userManager = userManager;
        _roleManager = roleManager;

    }
    [Authorize(Roles = "Admin")]
    public IActionResult Admin()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }
    

}



