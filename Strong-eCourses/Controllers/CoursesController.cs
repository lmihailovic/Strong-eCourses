using System.Diagnostics;
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

    // public IActionResult Index()
    // {
    //     var collection = _mongoService.GetCollection<Course>("courses");
    //     var courses = collection.Find(_ => true).ToList();


    //     return View(courses);
    // }

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

}