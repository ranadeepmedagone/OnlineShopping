using OnlineShopping.DTOs;

namespace OnlineShopping.Models;

public record Order
{
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public string Status { get; set; }
    
    public int TotalValue { get; set; }

    public string PaymentStatus { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }

    public long CustomerId { get; set; }


    public OrderDTO asDto => new OrderDTO
    {
          Id = Id,
          DeliveryDate = DeliveryDate,
          Status = Status,
          TotalValue = TotalValue,
          CreatedAt = CreatedAt,
          PaymentStatus = PaymentStatus,
          CustomerId = CustomerId
          
          

    };  
}