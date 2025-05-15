using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Strong_eCourses.Models;
using Strong_eCourses.Services;

namespace Strong_eCourses.Controllers;

public class CoursesController : Controller
{
    private readonly ILogger<CoursesController> _logger;

    private readonly MongoDBService _mongoService;

    public CoursesController(ILogger<CoursesController> logger, MongoDBService mongoService)
    {
        _logger = logger;
        _mongoService = mongoService;
    }

    public IActionResult Index()
    {
        var collection = _mongoService.GetCollection<Course>("courses");
        var courses = collection.Find(_ => true).ToList();

        
        return View(courses);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}