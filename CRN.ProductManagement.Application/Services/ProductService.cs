using AutoMapper;
using CRN.ProductManagement.Application.Common;
using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Domain.Entities;

namespace CRN.ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();

        return new ApiResponse<IEnumerable<ProductDto>>
        {
            Success = true,
            Message = "Products fetched successfully.",
            Data = _mapper.Map<IEnumerable<ProductDto>>(products)
        };
    }

    public async Task<ApiResponse<ProductDto>> GetByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse<ProductDto>
            {
                Success = false,
                Message = "Product not found."
            };
        }

        return new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product fetched successfully.",
            Data = _mapper.Map<ProductDto>(product)
        };
    }

    public async Task<ApiResponse<ProductDto>> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        product.CreatedBy = "System";
        product.CreatedOn = DateTime.UtcNow;

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product created successfully.",
            Data = _mapper.Map<ProductDto>(product)
        };
    }

    public async Task<ApiResponse<ProductDto>> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse<ProductDto>
            {
                Success = false,
                Message = "Product not found."
            };
        }

        product.ProductName = dto.ProductName;
        product.ModifiedBy = "System";
        product.ModifiedOn = DateTime.UtcNow;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<ProductDto>
        {
            Success = true,
            Message = "Product updated successfully.",
            Data = _mapper.Map<ProductDto>(product)
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Product not found."
            };
        }

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Product deleted successfully.",
            Data = true
        };
    }

    public async Task<ApiResponse<IEnumerable<ProductDto>>> GetProductsAsync(ProductQueryParameters queryParameters)
    {
        var products = await _unitOfWork.Products.GetProductsAsync(queryParameters);

        return new ApiResponse<IEnumerable<ProductDto>>
        {
            Success = true,
            Message = "Products fetched successfully.",
            Data = _mapper.Map<IEnumerable<ProductDto>>(products)
        };
    }
}