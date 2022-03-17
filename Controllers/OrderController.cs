using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DTOs;
using OnlineShopping.Models;
using OnlineShopping.Repositories;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _Order;
    private readonly IProductRepository _Product;
    

    public OrderController(ILogger<OrderController> logger,
    IOrderRepository Order,IProductRepository Product)
    {
        _logger = logger;
        _Order = Order;
        _Product = Product;
        
    }

    // [HttpGet]
    // public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
    // {
    //     var OrdersList = await _Order.GetList();

    //     // Order -> OrderDTO
    //     var dtoList = OrdersList.Select(x => x.asDto);

    //     return Ok(dtoList);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrderById([FromRoute] long id)
    {
        var Order = await _Order.GetById(id);

        if (Order is null)
            return NotFound("No Order found with given employee number");

            var dto = Order.asDto;
              dto.Product = (await _Product.GetProductsByOrderId(id)).Select(x => x.asDto).ToList(); 


        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO Data)
    {
        

        var toCreateOrder = new Order
        {
         CustomerId = Data.CustomerId,
         CreatedAt = Data.CreatedAt,
         Status = Data.Status,
         TotalValue = Data.TotalValue,
         PaymentStatus = Data.PaymentStatus,
        DeliveryDate = Data.DeliveryDate
           

        };

        var createdOrder = await _Order.Create(toCreateOrder);

        return StatusCode(StatusCodes.Status201Created, createdOrder.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder([FromRoute] long id,
    [FromBody] UpdateOrderDTO Data)
    {
        var existing = await _Order.GetById(id);
        if (existing is null)
            return NotFound("No Order found with given employee number");

        var toUpdateOrder = existing with
        {
            
            Status = Data.Status?.Trim() ?? existing.Status,
            TotalValue = Data.TotalValue,
            PaymentStatus = Data.PaymentStatus?.Trim() ?? existing.PaymentStatus,
            DeliveryDate = Data.DeliveryDate
           

            
            
        };

        var didUpdate = await _Order.Update(toUpdateOrder);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Order");

        return NoContent();
    }

//     [HttpDelete("{id}")]
//     public async Task<ActionResult> DeleteOrder([FromRoute] long id)
//     {
//         var existing = await _Order.GetById(id);
//         if (existing is null)
//             return NotFound("No Order found with given employee number");

//         var didDelete = await _Order.Delete(id);

//         return NoContent();
//     }
}
