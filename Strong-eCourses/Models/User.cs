using System.ComponentModel.DataAnnotations;
namespace Strong_eCourses;

public class User
{
    [Required]
    public string FirstName{ get; set; }

    [Required]
    public string LastName{ get; set; }

    [Required]
    public string Email{ get; set; }

    [Required]
    public string Password{get;set;} 
}
