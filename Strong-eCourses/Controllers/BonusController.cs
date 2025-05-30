using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Strong_eCourses.Models;
using Strong_eCourses.Services;
using Microsoft.AspNetCore.Authorization;

namespace Strong_eCourses.Controllers;

public class BonusController : Controller
{
    private readonly MongoDBService _mongoService;

    public BonusController(MongoDBService mongoService)
    {
        _mongoService = mongoService;
    }

    [Authorize]
    public IActionResult Index()
    {
        var collection = _mongoService.GetCollection<Course>("courses");

        var viewModel = new CourseViewModel
        {
            Courses = collection.Find(Builders<Course>.Filter.Eq(c => c.Category, "Programming")).ToList(),
        };
        
        return View(viewModel);
    }
}