using CRN.ProductManagement.Application.Common;
using CRN.ProductManagement.Application.DTOs;

namespace CRN.ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<IEnumerable<ProductDto>>> GetAllAsync();

    Task<ApiResponse<ProductDto>> GetByIdAsync(int id);

    Task<ApiResponse<ProductDto>> CreateAsync(CreateProductDto dto);

    Task<ApiResponse<ProductDto>> UpdateAsync(int id, UpdateProductDto dto);

    Task<ApiResponse<bool>> DeleteAsync(int id);

    Task<ApiResponse<IEnumerable<ProductDto>>> GetProductsAsync(ProductQueryParameters queryParameters);
}