namespace CatalogService.Application.Dto
{
    public class CatalogItemInput
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CatalogCategoryId { get; set; }

        public int AvailableInStock { get; set; }

        public bool OnReorder { get; set; }
    }
}
