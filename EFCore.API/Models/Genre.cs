using System.Text.Json.Serialization;

namespace EFCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [JsonIgnore] 
    public DateTime CreatedDate { get; set; }

    // Navigation properties that allow us to querry all of the movies that have the same genre
    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}