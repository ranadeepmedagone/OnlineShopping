using System.Text.Json.Serialization;

namespace OnlineShopping.DTOs;

public record CustomerDTO
{
    public long Id { get; set; }

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public long Mobile { get; set; }

    public string Password { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("order")]
    public List<OrderDTO> Order { get; set; }


    
}

public record CreateCustomerDTO
{
    

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public long Mobile { get; set; }

    public string Password { get; set; }

    


    
}

public record UpdateCustomerDTO
{
    

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public long Mobile { get; set; }

    public string Password { get; set; }


    
}