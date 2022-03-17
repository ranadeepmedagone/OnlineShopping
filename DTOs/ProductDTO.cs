using System.Text.Json.Serialization;
using OnlineShopping.Models;

namespace OnlineShopping.DTOs;

public record ProductDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Size { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string InStock { get; set; }
     
     [JsonPropertyName("tags")]
    public List<TagDTO> Tags { get; set; }


}
public record CreateProductDTO
{
    

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Size { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string InStock { get; set; }
}


public record UpdateProductDTO
{
    

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Size { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string InStock { get; set; }
}


