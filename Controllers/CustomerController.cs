using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DTOs;
using OnlineShopping.Models;
using OnlineShopping.Repositories;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _Customer;
    private readonly IOrderRepository _order;
    

    public CustomerController(ILogger<CustomerController> logger,
    ICustomerRepository Customer,IOrderRepository order)
    {
        _logger = logger;
        _Customer = Customer;
        _order = order;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
    {
        var CustomersList = await _Customer.GetList();

        // Customer -> CustomerDTO
        var dtoList = CustomersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] long id)
    {
        var Customer = await _Customer.GetById(id);

        if (Customer is null)
            return NotFound("No Customer found with given employee number");

            var dto = Customer.asDto;
             
              dto.Order = (await _order.GetOrderByCustomerId(id)).Select(x => x.asDto).ToList(); 



        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO Data)
    {
        

        var toCreateCustomer = new Customer
        {
           CustomerName = Data.CustomerName,
           Email = Data.Email,
           Mobile = Data.Mobile,
           Password = Data.Password

        };

        var createdCustomer = await _Customer.Create(toCreateCustomer);

        return StatusCode(StatusCodes.Status201Created, createdCustomer.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer([FromRoute] long id,
    [FromBody] UpdateCustomerDTO Data)
    {
        var existing = await _Customer.GetById(id);
        if (existing is null)
            return NotFound("No Customer found with given employee number");

        var toUpdateCustomer = existing with
        {
            
            CustomerName = Data.CustomerName?.Trim() ?? existing.CustomerName,
            Email = Data.Email?.Trim() ?? existing.Email,
            Mobile = Data.Mobile,
            Password = Data.Password
            
        };

        var didUpdate = await _Customer.Update(toUpdateCustomer);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Customer");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute] long id)
    {
        var existing = await _Customer.GetById(id);
        if (existing is null)
            return NotFound("No Customer found with given employee number");

        var didDelete = await _Customer.Delete(id);

        return NoContent();
    }
}
