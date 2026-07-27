using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRN.ProductManagement.Infrastructure.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Product?> GetByProductNameAsync(string productName)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.ProductName == productName);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters queryParameters)
    {
        IQueryable<Product> query = _context.Products.AsNoTracking();

        // Search
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            query = query.Where(x =>
                x.ProductName.Contains(queryParameters.Search));
        }

        // Sorting
        query = queryParameters.SortBy?.ToLower() switch
        {
            "name" => queryParameters.Descending
                ? query.OrderByDescending(x => x.ProductName)
                : query.OrderBy(x => x.ProductName),

            "createdon" => queryParameters.Descending
                ? query.OrderByDescending(x => x.CreatedOn)
                : query.OrderBy(x => x.CreatedOn),

            _ => query.OrderBy(x => x.Id)
        };

        // Pagination
        query = query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);

        return await query.ToListAsync();
    }

  /*  public async Task<IEnumerable<Product>> GetProductsAsync(ProductQueryParameters queryParameters)
    {
        IQueryable<Product> query = _context.Products.AsNoTracking();

        // Search
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            query = query.Where(x =>
                x.ProductName.Contains(queryParameters.Search));
        }

        // Sorting
        query = queryParameters.SortBy?.ToLower() switch
        {
            "name" => queryParameters.Descending
                ? query.OrderByDescending(x => x.ProductName)
                : query.OrderBy(x => x.ProductName),

            "createdon" => queryParameters.Descending
                ? query.OrderByDescending(x => x.CreatedOn)
                : query.OrderBy(x => x.CreatedOn),

            _ => query.OrderBy(x => x.Id)
        };

        // Pagination
        query = query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);

        return await query.ToListAsync();
    }*/
}