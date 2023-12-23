using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Application.Exceptions;
using CatalogService.Entities;
using CatalogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatalogService.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogDbContext _dbContext;
        private readonly IMapper _mapper;

        public CatalogService(CatalogDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<CatalogCategory> AddCategory(CatalogCategoryInput input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            var newCatalogCategory = _mapper.Map<CatalogCategory>(input);

            _dbContext.CatalogCategories.Add(newCatalogCategory);
            await _dbContext.SaveChangesAsync();

            return newCatalogCategory;
        }

        public async Task<CatalogItem> AddItem(CatalogItemInput input)
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input));

            var catalogCategoryExist = await _dbContext.CatalogCategories.AnyAsync(x => x.Id == input.CatalogCategoryId);
            if (!catalogCategoryExist)
                throw new EntityNotFoundException($"Catalog Category with ID: {input.CatalogCategoryId} doesn't exist.");

            var newCatalogItem = _mapper.Map<CatalogItem>(input);

            _dbContext.CatalogItems.Add(newCatalogItem);
            await _dbContext.SaveChangesAsync();

            return newCatalogItem;
        }

        public async Task DeleteCategory(int id)
        {
            var category = _dbContext.CatalogCategories.SingleOrDefault(x => x.Id == id);

            if (category is null)
                throw new EntityNotFoundException($"Catalog Category with ID: {id} not found.");

            _dbContext.CatalogCategories.Remove(category);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteItem(int id)
        {
            var item = _dbContext.CatalogItems.SingleOrDefault(x => x.Id == id);

            if (item is null)
                throw new EntityNotFoundException($"Item with id {id} not found.");

            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CatalogCategory>> GetCategories()
        {
            return await _dbContext.CatalogCategories.ToListAsync();
        }

        public async Task<PaginatedItemsDto<CatalogItem>> GetItems(CatalogItemsQuery query)
        {
            Expression<Func<CatalogItem, bool>> predicate = x => query.CatalogCategoryId == null ? true : x.CatalogCategoryId == query.CatalogCategoryId;

            var totalItems = await _dbContext.CatalogItems.CountAsync(predicate);
            var items = await _dbContext.CatalogItems
                .AsNoTracking()
                .Where(predicate)
                .Skip(query.PageNumber * query.PageNumber)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedItemsDto<CatalogItem>(query.PageNumber, query.PageSize, totalItems, items);
        }

        public async Task UpdateCategory(int categoryId, CatalogCategoryInput input)
        {
            var category = await _dbContext.CatalogCategories.SingleOrDefaultAsync(i => i.Id == categoryId);

            if (category == null)
                throw new EntityNotFoundException($"Category with id {categoryId} not found.");

            _mapper.Map(input, category);
            _dbContext.CatalogCategories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateItem(int itemId, CatalogItemInput input)
        {
            var item = await _dbContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
                throw new EntityNotFoundException($"Item with id {itemId} not found.");

            _mapper.Map(input, item);
            _dbContext.CatalogItems.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CatalogCategory> GetCategoryById(int id)
        {
            var category = await _dbContext.CatalogCategories
                .FirstOrDefaultAsync(i => i.Id == id);

            if (category is null)
                throw new EntityNotFoundException($"Category with id {id} not found.");

            return category;
        }

        public async Task<CatalogItem> GetItemById(int id)
        {
            var item = await _dbContext.CatalogItems    
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item is null)
                throw new EntityNotFoundException($"Item with id {id} not found.");

            return item;
        }
    }
}
