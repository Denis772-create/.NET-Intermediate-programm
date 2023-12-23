namespace CatalogService.Application.Dto;

public class PaginatedItemsDto<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public List<T> Items { get; }

    public PaginatedItemsDto(int pageIndex, int pageSize, int totalItems, List<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
        Items = items;
    }
}
