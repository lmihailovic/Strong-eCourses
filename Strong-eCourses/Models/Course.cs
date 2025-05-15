using System.ComponentModel.DataAnnotations;
namespace Strong_eCourses.Models;

public class Course
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Link { get; set; }
    
    [Required]
    public string Difficulty { get; set; }

    [Required]
    public List<string> Categories { get; set; }
}