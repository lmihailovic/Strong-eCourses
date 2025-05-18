
namespace Strong_eCourses.Models;


public class CourseViewModel
{
    public List<Course> Courses { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
