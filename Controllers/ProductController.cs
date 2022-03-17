using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DTOs;
using OnlineShopping.Models;
using OnlineShopping.Repositories;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _Product;
    private readonly IOrderRepository _Order;
    private readonly ITagRepository _tags;
    

    public ProductController(ILogger<ProductController> logger,
    IProductRepository Product,IOrderRepository Order, ITagRepository tags)
    {
        _logger = logger;
        _Product = Product;
        _Order = Order;
        _tags = tags;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
    {
        var ProductsList = await _Product.GetList();

        // Product -> ProductDTO
        var dtoList = ProductsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById([FromRoute] long id)
    {
        var Product = await _Product.GetById(id);

        if (Product is null)
            return NotFound("No Product found with given employee number");

            var dto = Product.asDto;
            //   dto.Order = (await _Order.GetOrdersByProductId(id)).Select(x => x.asDto).ToList(); 
              dto.Tags = (await _tags.GetTagsByProductId(id)).Select(x => x.asDto).ToList(); 



        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO Data)
    {
        

        var toCreateProduct = new Product
        {
           Name = Data.Name,
           Price = Data.Price,
           Size = Data.Size,
           CreatedAt = Data.CreatedAt,
           InStock = Data.InStock

           

        };

        var createdProduct = await _Product.Create(toCreateProduct);

        return StatusCode(StatusCodes.Status201Created, createdProduct.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] long id,
    [FromBody] UpdateProductDTO Data)
    {
        var existing = await _Product.GetById(id);
        if (existing is null)
            return NotFound("No Product found with given employee number");

        var toUpdateProduct = existing with
        {
            
            Name = Data.Name?.Trim() ?? existing.Name,
            Price = Data.Price,
            Size = Data.Size,
            CreatedAt = Data.CreatedAt,
            InStock = Data.InStock

            
            
        };

        var didUpdate = await _Product.Update(toUpdateProduct);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Product");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] long id)
    {
        var existing = await _Product.GetById(id);
        if (existing is null)
            return NotFound("No Product found with given employee number");

        var didDelete = await _Product.Delete(id);

        return NoContent();
    }
}
