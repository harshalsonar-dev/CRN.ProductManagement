using System.Reflection;
using AutoMapper;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRN.ProductManagement.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}