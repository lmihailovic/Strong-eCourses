
namespace Strong_eCourses.Models;


public class CourseViewModel
{
    public List<Course> Courses { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public string SearchQuery { get; set; }
    public string SelectedCategory { get; set; }
    public List<string> Categories { get; set; }

}
