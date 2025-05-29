using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Strong_eCourses.Models;
using Strong_eCourses.Services;
using Microsoft.AspNetCore.Authorization;

namespace Strong_eCourses.Controllers;

public class BonusController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}