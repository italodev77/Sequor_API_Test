using AutoMapper;
using Sequor.Application.DTOs;
using Sequor.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Material, MaterialDTO>();
        CreateMap<ProductMaterial, MaterialDTO>()
            .ForMember(dest => dest.MaterialCode, opt => opt.MapFrom(src => src.MaterialCode))
            .ForMember(dest => dest.MaterialDescription, opt => opt.MapFrom(src => src.Material.MaterialDescription));

        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.ProductDescription))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image))
            .ForMember(dest => dest.CycleTime, opt => opt.MapFrom(src => src.Product.CycleTime))
            .ForMember(dest => dest.Materials, opt => opt.MapFrom(src => src.Product.ProductMaterials));

        CreateMap<Production, ProductionItemDTO>()
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

        
        CreateMap<IEnumerable<Production>, GetProductionResponseDTO>()
            .ForMember(dest => dest.Productions, opt => opt.MapFrom(src => src));
    }
}
