namespace CatalogService.ViewModel;

public class PaginatedItemsViewModel<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public List<T> Items { get; }

    public PaginatedItemsViewModel(int pageIndex, int pageSize, int totalItems, List<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalItems = totalItems;
        Items = items;
    }
}
