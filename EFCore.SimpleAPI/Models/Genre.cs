using System.Text.Json.Serialization;

namespace EFCore.SimpleAPI.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}