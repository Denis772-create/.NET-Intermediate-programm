using CatalogService.Entities;

namespace CatalogService.Infrastructure;

public class CatalogDbContextSeed
{
    public async Task SeedAsync(CatalogDbContext context)
    {
        if (!context.CatalogCategories.Any())
        {
            await context.CatalogCategories.AddRangeAsync(GetPreconfiguredCatalogCategories());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private IEnumerable<CatalogCategory> GetPreconfiguredCatalogCategories()
    {
        return new List<CatalogCategory>
        {
            new() { Name = "Electronics" },
            new() { Name = "Clothing" },
            new() { Name = "Home Decor" },
            new() { Name = "Accessories" }
        };
    }

    private IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
        {
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Smartphone", Name = "Smartphone", Price = 599.99M },
            new() { CatalogCategoryId = 1, AvailableInStock = 100, Description = "Wireless Earbuds", Name = "Wireless Earbuds", Price = 129.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Vintage Leather Jacket", Name = "Vintage Leather Jacket", Price = 199.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Stylish Sunglasses", Name = "Stylish Sunglasses", Price = 49.99M },
            new() { CatalogCategoryId = 3, AvailableInStock = 100, Description = "Cozy Throw Blanket", Name = "Cozy Throw Blanket", Price = 39.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Graphic Print Hoodie", Name = "Graphic Print Hoodie", Price = 79.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Classic Red Polo Shirt", Name = "Classic Red Polo Shirt", Price = 29.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Leather Wallet", Name = "Leather Wallet", Price = 49.99M },
            new() { CatalogCategoryId = 1, AvailableInStock = 100, Description = "Coffee Maker", Name = "Coffee Maker", Price = 89.99M },
            new() { CatalogCategoryId = 3, AvailableInStock = 100, Description = "Soft Bed Sheets", Name = "Soft Bed Sheets", Price = 59.99M },
            new() { CatalogCategoryId = 3, AvailableInStock = 100, Description = "Decorative Pillow Set", Name = "Decorative Pillow Set", Price = 34.99M },
            new() { CatalogCategoryId = 2, AvailableInStock = 100, Description = "Sporty Jogging Pants", Name = "Sporty Jogging Pants", Price = 44.99M }
        };
    }
}