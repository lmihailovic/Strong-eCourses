using System.Diagnostics;
using DnsClient.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Strong_eCourses.Models;
using Strong_eCourses.Services;

namespace Strong_eCourses.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ILogger<CoursesController> _logger;

    private readonly MongoDBService _mongoService;

    public CoursesController(ILogger<CoursesController> logger, MongoDBService mongoService)
    {
        _logger = logger;
        _mongoService = mongoService;
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

    public IActionResult Index(string searchQuery = "", string selectedCategory = "", int page = 1, int pageSize = 6)
    {
        // Ucitavanje kategorija
        var collection = _mongoService.GetCollection<Course>("courses");

        var categories = collection.Distinct<string>("Category", FilterDefinition<Course>.Empty).ToList();


        var filterBuilder = Builders<Course>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            filter &= filterBuilder.Regex(c => c.Name, new MongoDB.Bson.BsonRegularExpression(searchQuery, "i"));
        }

        if (!string.IsNullOrWhiteSpace(selectedCategory))
        {
            filter &= filterBuilder.Eq(c => c.Category, selectedCategory);
        }

        // Paginacija
        var totalCourses = collection.CountDocuments(filter);
        var totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);


        var courses = collection
            .Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToList();

        var viewModel = new CourseViewModel
        {
            Courses = courses,
            CurrentPage = page,
            TotalPages = totalPages,
            SearchQuery = searchQuery,
            SelectedCategory = selectedCategory,
            Categories = categories
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var collection = _mongoService.GetCollection<Course>("courses");

        await collection.DeleteOneAsync(p => p.Id == id);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Course()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Course(Course course)
    {
        var collection = _mongoService.GetCollection<Course>("courses");

        if (!ModelState.IsValid)
        {
            return View(course);
        }
        await collection.InsertOneAsync(course);

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var collection = _mongoService.GetCollection<Course>("courses");
        var course = await collection.Find(c => c.Id == id).FirstOrDefaultAsync();

        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Edit(Course course)
    {
        if (!ModelState.IsValid)
        {
            return View(course);
        }

        var collection = _mongoService.GetCollection<Course>("courses");

        var filter = Builders<Course>.Filter.Eq(c => c.Id, course.Id);

        // Zameni postojeći dokument novim izmenjenim podacima
        var result = await collection.ReplaceOneAsync(filter, course);

        if (result.MatchedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }
        var collection = _mongoService.GetCollection<Course>("courses");
        
        var course = await collection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

}