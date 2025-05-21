using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strong_eCourses.Models;

namespace Strong_eCourses.Controllers;
[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
