using System.ComponentModel.DataAnnotations;

public class Course
{
    [Required]
    public long Id { get; private set; }

    [Required]
    public string Name { get; private set; }

    [Required]
    public string Link { get; private set; }
    
    [Required]
    public string Difficulty { get; private set; }

    [Required]
    public List<string> Categories { get; private set; }
}