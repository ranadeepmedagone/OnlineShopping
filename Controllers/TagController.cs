using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DTOs;
using OnlineShopping.Models;
using OnlineShopping.Repositories;

namespace Dotsql.Controllers;

[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagRepository _Tag;
    

    public TagController(ILogger<TagController> logger,
    ITagRepository Tag)
    {
        _logger = logger;
        _Tag = Tag;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDTO>>> GetAllTags()
    {
        var TagsList = await _Tag.GetList();

        // Tag -> TagDTO
        var dtoList = TagsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDTO>> GetTagById([FromRoute] long id)
    {
        var Tag = await _Tag.GetById(id);

        if (Tag is null)
            return NotFound("No Tag found with given employee number");



        return Ok(Tag.asDto);
    }

    [HttpPost]
    public async Task<ActionResult<TagDTO>> CreateTag([FromBody] CreateTagDTO Data)
    {
        

        var toCreateTag = new Tag
        {
         Name = Data.Name
           

        };

        var createdTag = await _Tag.Create(toCreateTag);

        return StatusCode(StatusCodes.Status201Created, createdTag.asDto);
    }

    // [HttpPut("{id}")]
    // public async Task<ActionResult> UpdateTag([FromRoute] long id,
    // [FromBody] UpdateTagDTO Data)
    // {
    //     var existing = await _Tag.GetById(id);
    //     if (existing is null)
    //         return NotFound("No Tag found with given employee number");

    //     var toUpdateTag = existing with
    //     {
            
    //         Status = Data.Status?.Trim() ?? existing.Status,
    //         TotalValue = Data.TotalValue,
    //         PaymentStatus = Data.PaymentStatus?.Trim() ?? existing.PaymentStatus,
    //         DeliveryDate = Data.DeliveryDate
           

            
            
    //     };

    //     var didUpdate = await _Tag.Update(toUpdateTag);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Tag");

    //     return NoContent();
    // }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag([FromRoute] long id)
    {
        var existing = await _Tag.GetById(id);
        if (existing is null)
            return NotFound("No Tag found with given employee number");

        var didDelete = await _Tag.Delete(id);

        return NoContent();
    }
}
