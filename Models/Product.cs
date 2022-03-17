using OnlineShopping.DTOs;

namespace OnlineShopping.Models;

public record Product
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
    
    public int Size { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string InStock { get; set; }


    public ProductDTO asDto => new ProductDTO
    {
          Id = Id,
          Name = Name,
          Price = Price,
          Size = Size,
          CreatedAt = CreatedAt,
          InStock = InStock

    };  
}