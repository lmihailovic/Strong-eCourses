using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strong_eCourses.Models;
using Strong_eCourses.Services;

namespace Strong_eCourses.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly MongoDBService _mongoService;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
    MongoDBService mongoService)
    {

        _userManager = userManager;
        _roleManager = roleManager;
        _mongoService = mongoService;

    }
    [Authorize(Roles = "Admin")]
    public IActionResult Admin()
    {
        //var users = _userManager.Users.ToList();
        
        //var courses = _mongoService.GetCollection<Course>("courses");

        return View();
    }

    public IActionResult Users()
    {
        return View();
    }

    public IActionResult NewCourse()
    {
        return View();
    }

}



