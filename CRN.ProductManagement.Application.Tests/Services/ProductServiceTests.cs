using AutoMapper;
using Moq;
using Xunit;
using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Application.Mappings;
using CRN.ProductManagement.Application.Services;
using CRN.ProductManagement.Application.Tests.Mocks;
using CRN.ProductManagement.Domain.Entities;

namespace CRN.ProductManagement.Application.Tests.Services;

public class ProductServiceTests
{
    private readonly ProductService _productService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly IMapper _mapper;

    public ProductServiceTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        _productRepository = new Mock<IProductRepository>();

        _unitOfWork
            .Setup(x => x.Products)
            .Returns(_productRepository.Object);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductMappingProfile>();
        });

        _mapper = configuration.CreateMapper();

        _productService =
            new ProductService(
                _unitOfWork.Object,
                _mapper);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "System",
            CreatedOn = DateTime.UtcNow
        };

        _productRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(product);

        // Act
        var result = await _productService.GetByIdAsync(1);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Laptop", result.Data.ProductName);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Product()
    {
        // Arrange
        var dto = new CreateProductDto
        {
            ProductName = "Keyboard"
        };

        // Act
        var result = await _productService.CreateAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Keyboard", result.Data!.ProductName);

        _productRepository.Verify(
            x => x.AddAsync(It.IsAny<Product>()),
            Times.Once);

        _unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_False_When_Product_Not_Found()
    {
        // Arrange
        _productRepository
            .Setup(x => x.GetByIdAsync(10))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _productService.DeleteAsync(10);

        // Assert
        Assert.False(result.Success);
    }
}