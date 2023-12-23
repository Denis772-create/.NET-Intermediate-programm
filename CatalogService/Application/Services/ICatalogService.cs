using CatalogService.Application.Dto;
using CatalogService.Entities;

namespace CatalogService.Application.Services
{
    public interface ICatalogService
    {
        Task<List<CatalogCategory>> GetCategories();
        Task<PaginatedItemsDto<CatalogItem>> GetItems(CatalogItemsQuery query);
        Task<CatalogItem> AddItem(CatalogItemInput input);
        Task<CatalogCategory> AddCategory(CatalogCategoryInput input);
        Task UpdateItem(int itemId, CatalogItemInput input);
        Task UpdateCategory(int categoryId, CatalogCategoryInput input);
        Task DeleteCategory(int id);
        Task DeleteItem(int id);
        Task<CatalogCategory> GetCategoryById(int id);
        Task<CatalogItem> GetItemById(int id);
    }
}
