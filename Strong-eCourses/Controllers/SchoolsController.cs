
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Strong_eCourses.Models;
using Strong_eCourses.Services;
using Microsoft.Extensions.Logging;
using System;

namespace Strong_eCourses.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly ILogger<SchoolsController> _logger;
        private readonly MongoDBService _mongoService;

        public SchoolsController(ILogger<SchoolsController> logger, MongoDBService mongoService)
        {
            _logger = logger;
            _mongoService = mongoService;
        }

        public IActionResult Index(string selectedSchool = "", int page = 1, int pageSize = 6)
        {
            var collection = _mongoService.GetCollection<Course>("courses");

            // Distinct polje "School" sa velikim S
            var schools = collection.Distinct<string>("school", FilterDefinition<Course>.Empty).ToList();

            var filterBuilder = Builders<Course>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(selectedSchool))
            {
                filter &= filterBuilder.Eq(c => c.School, selectedSchool);
            }

            // Pagination
            var totalCourses = collection.CountDocuments(filter);
            var totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

            var courses = collection
                .Find(filter)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToList();

            var viewModel = new CourseFilterModel
            {
                Schools = schools,
                SelectedSchool = selectedSchool,
                Courses = courses,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}
