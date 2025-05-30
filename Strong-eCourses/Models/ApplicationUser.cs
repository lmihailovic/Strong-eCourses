using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
namespace Strong_eCourses;
[CollectionName("Users")]
public class ApplicationUser:MongoIdentityUser<Guid>
{

}
