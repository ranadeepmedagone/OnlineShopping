using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShopping.Models;

public record TagDTO
{
    [JsonPropertyName("id")]
    [Required]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }




}

public record CreateTagDTO
{
    
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }




}