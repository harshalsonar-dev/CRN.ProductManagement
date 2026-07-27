using AutoMapper;
using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Domain.Entities;

namespace CRN.ProductManagement.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductDto, Product>();

        CreateMap<UpdateProductDto, Product>();
    }
}