namespace CatalogService.Entities;

public class CatalogItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CatalogCategoryId { get; set; }

    public CatalogCategory CatalogCategory { get; set; }

    public int AvailableInStock { get; set; }

    public bool OnReorder { get; set; }

    public CatalogItem() { }
}