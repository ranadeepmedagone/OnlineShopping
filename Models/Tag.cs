using OnlineShopping.DTOs;

namespace OnlineShopping.Models;

public record Tag
{
    public long Id { get; set; }

    public string Name { get; set; }




    public TagDTO asDto => new TagDTO
    {
          Id = Id,
          Name = Name
          
          

    };  
}