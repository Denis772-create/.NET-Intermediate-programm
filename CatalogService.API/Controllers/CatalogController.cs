using CatalogService.Application.Dto;
using CatalogService.Application.Services;
using CatalogService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json", "application/xml")]
[Consumes("application/json", "application/xml")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(CacheProfileName = "Default10")]
    public async Task<ActionResult<List<CatalogCategory>>> GetCatalogCategories()
        => await _catalogService.GetCategories();

    [HttpGet("items")]
    [ProducesResponseType(typeof(PaginatedItemsDto<CatalogItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ResponseCache(CacheProfileName = "Default10")]
    public async Task<ActionResult<PaginatedItemsDto<CatalogItem>>> GetItems([FromQuery] CatalogItemsQuery query)
        => await _catalogService.GetItems(query);

    [HttpPost("categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCategory([FromBody] CatalogCategoryInput categoryInput)
    {
        var newCatalogCategory = await _catalogService.AddCategory(categoryInput);

        return CreatedAtAction(nameof(GetCategoryById),
            new { id = newCatalogCategory.Id }, newCatalogCategory);
    }

    [HttpPost("items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateItem([FromBody] CatalogItemInput itemInput)
    {
        var catalogCategory = await _catalogService.AddItem(itemInput);

        return CreatedAtAction(nameof(GetItemById),
            new { id = catalogCategory.Id }, catalogCategory);
    }

    [HttpPut("items/{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateItem([FromRoute] int itemId, [FromBody] CatalogItemInput itemInput)
    {
        await _catalogService.UpdateItem(itemId, itemInput);
        return NoContent();
    }

    [HttpPut("categories/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] CatalogCategoryInput categoryToUpdate)
    {
        await _catalogService.UpdateCategory(categoryId, categoryToUpdate);
        return NoContent();
    }

    [HttpDelete("items/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _catalogService.DeleteItem(id);
        return NoContent();
    }

    [Route("categories/{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        await _catalogService.DeleteCategory(id);
        return NoContent();
    }

    [HttpGet("items/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItem>> GetItemById(int id)
        => await _catalogService.GetItemById(id);

    [HttpGet("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogCategory>> GetCategoryById(int id)
        => await _catalogService.GetCategoryById(id);
}