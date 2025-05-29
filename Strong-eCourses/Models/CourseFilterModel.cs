using System.Collections.Generic;

namespace Strong_eCourses.Models
{
    public class CourseFilterModel
    {
        public List<string> Schools { get; set; } = new List<string>();
        public string? SelectedSchool { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

    }
}
