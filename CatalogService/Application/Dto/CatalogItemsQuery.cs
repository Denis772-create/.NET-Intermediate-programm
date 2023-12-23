namespace CatalogService.Application.Dto
{
    public class CatalogItemsQuery : PagedQueryParams
    {
        public int? CatalogCategoryId { get; set; }
    }
}
