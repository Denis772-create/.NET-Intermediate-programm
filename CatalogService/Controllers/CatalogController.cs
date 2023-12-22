using CatalogService.Entities;
using CatalogService.Infrastructure;
using CatalogService.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly CatalogDbContext _dbContext;

    public CatalogController(CatalogDbContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));

        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CatalogCategory>>> GetCatalogCategories()
        => await _dbContext.CatalogCategories.ToListAsync();

    [HttpGet("items")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetItems([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalItems = await _dbContext.CatalogItems.CountAsync();
        var items = await _dbContext.CatalogItems
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, items);

        return Ok(model);
    }

    [HttpPost("categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCategory([FromBody] CatalogCategory category)
    {
        _dbContext.CatalogCategories.Add(category);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoryById),
            new { id = category.Id }, category);
    }

    [HttpPost("items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateItem([FromBody] CatalogItem Item)
    {
        _dbContext.CatalogItems.Add(Item);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetItemById),
            new { id = Item.Id }, Item);
    }

    [HttpPut("items")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateItem([FromBody] CatalogItem ItemToUpdate)
    {
        var catalogItem = await _dbContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == ItemToUpdate.Id);

        if (catalogItem == null)
            return NotFound(new { Message = $"Item with id {ItemToUpdate.Id} not found." });

        catalogItem = ItemToUpdate;
        _dbContext.CatalogItems.Update(catalogItem);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("categories")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateCategory([FromBody] CatalogCategory categoryToUpdate)
    {
        var catalogCategory = await _dbContext.CatalogCategories.SingleOrDefaultAsync(i => i.Id == categoryToUpdate.Id);

        if (catalogCategory == null)
            return NotFound(new { Message = $"Category with id {categoryToUpdate.Id} not found." });

        catalogCategory = categoryToUpdate;
        _dbContext.CatalogCategories.Update(catalogCategory);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("items/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteItem(int id)
    {
        var item = _dbContext.CatalogItems.SingleOrDefault(x => x.Id == id);

        if (item is null)
            return NotFound();

        _dbContext.CatalogItems.Remove(item);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [Route("categories/{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = _dbContext.CatalogCategories.SingleOrDefault(x => x.Id == id);

        if (category is null)
            return NotFound();

        _dbContext.CatalogCategories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("items/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItem>> GetItemById(int id)
    {
        var item = await _dbContext.CatalogItems
            .FirstOrDefaultAsync(i => i.Id == id);

        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CatalogItem>> GetCategoryById(int id)
    {
        var item = await _dbContext.CatalogCategories
            .FirstOrDefaultAsync(i => i.Id == id);

        return item is null ? NotFound() : Ok(item);
    }
}