using AutoMapper;
using CatalogService.Application.Dto;
using CatalogService.Entities;

namespace CatalogService.Application.Mappers
{
    public class CatalogItemProfile : Profile
    {
        public CatalogItemProfile()
        {
            CreateMap<CatalogItem, CatalogItemInput>()
                .ReverseMap()
                .ForMember(x => x.Id, x => x.Ignore());
        }
    }
}
