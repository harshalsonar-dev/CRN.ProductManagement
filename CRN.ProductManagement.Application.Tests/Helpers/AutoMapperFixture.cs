using AutoMapper;
using CRN.ProductManagement.Application.Mappings;

namespace CRN.ProductManagement.Application.Tests.Helpers;

public static class AutoMapperFixture
{
    public static IMapper GetMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductMappingProfile>();
        });

        configuration.AssertConfigurationIsValid();

        return configuration.CreateMapper();
    }
}