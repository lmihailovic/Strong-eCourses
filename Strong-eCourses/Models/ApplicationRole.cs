using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
namespace Strong_eCourses;

[CollectionName("Roles")]
public class ApplicationRole:MongoIdentityRole<Guid>
{

}
