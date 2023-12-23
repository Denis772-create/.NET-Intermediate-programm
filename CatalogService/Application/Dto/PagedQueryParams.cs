namespace CatalogService.Application.Dto
{
    public class PagedQueryParams
    {
        private const int DEFAULT_PAGE_NUMBER = 0;
        private const int DEFAULT_PAGE_SIZE = 10;

        public PagedQueryParams()
        {
        }

        public int PageNumber { get; set; } = DEFAULT_PAGE_NUMBER;

        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
    }
}
