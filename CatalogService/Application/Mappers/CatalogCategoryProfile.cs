using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Entities;

namespace CatalogService.Application.Mappers
{
    internal class CatalogCategoryProfile : Profile
    {
        public CatalogCategoryProfile()
        {
            CreateMap<CatalogCategory, CatalogCategoryInput>()
                .ReverseMap()
                .ForMember(x => x.Id, x => x.Ignore());
        }
    }
}
