using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShopping.DTOs;

public record OrderDTO
{
    [JsonPropertyName("id")]
    [Required]
    public long Id { get; set; }
    [JsonPropertyName("created_at")]
    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    [JsonPropertyName("status")]
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
    [JsonPropertyName("total_value")]
    [Required]
    public int TotalValue { get; set; }
    [JsonPropertyName("payment_status")]
    [Required]
    public string PaymentStatus { get; set; }

    [JsonPropertyName("delivery_date")]
    [Required]
    public DateTimeOffset DeliveryDate { get; set; }

    [JsonPropertyName("customer_id")]
    [Required]


    public long CustomerId { get; set; }

    [JsonPropertyName("product")]
    [Required]
    public List<ProductDTO> Product { get; set; }



    
}

public record CreateOrderDTO
{

    [JsonPropertyName("created_at")]
    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    [JsonPropertyName("status")]
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
    [JsonPropertyName("total_value")]
    [Required]
    public int TotalValue { get; set; }
    [JsonPropertyName("payment_status")]
    [Required]
    public string PaymentStatus { get; set; }

    [JsonPropertyName("delivery_date")]
    [Required]
    public DateTimeOffset DeliveryDate { get; set; }

    [JsonPropertyName("customer_id")]
    [Required]


    public long CustomerId { get; set; }


    
}

public record UpdateOrderDTO
{
    
    
    [JsonPropertyName("status")]
    [Required]
    [MaxLength(50)]
    public string Status { get; set; }
    [JsonPropertyName("total_value")]
    [Required]
    public int TotalValue { get; set; }
    [JsonPropertyName("payment_status")]
    [Required]
    public string PaymentStatus { get; set; }

    [JsonPropertyName("delivery_date")]
    [Required]
    public DateTimeOffset DeliveryDate { get; set; }


    
}