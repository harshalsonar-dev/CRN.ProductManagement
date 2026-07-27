using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Domain.Entities;
// using CRN.ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRN.ProductManagement.Application.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetByProductNameAsync(string productName);

    Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters queryParameters);

    //Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters queryParameters);
}