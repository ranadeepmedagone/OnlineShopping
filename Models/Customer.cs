using OnlineShopping.DTOs;

namespace OnlineShopping.Models;

public record Customer
{
    public long Id { get; set; }

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public long Mobile { get; set; }

    public string Password { get; set; }

    public DateTimeOffset CreatedAt { get; set; }


    public CustomerDTO asDto => new CustomerDTO
    {
          Id = Id,
          CustomerName = CustomerName,
          Email = Email,
          Mobile = Mobile,
          Password = Password,
          CreatedAt = CreatedAt

    };  
    
}