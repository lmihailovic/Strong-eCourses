using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Strong_eCourses.Models;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("link")]
    public string Link { get; set; }

    [BsonElement("category")]
    public string Category { get; set; }

    [BsonElement("difficulty")]
    public string Difficulty { get; set; }

    [BsonElement("school")]
    public string School { get; set; }

    
}