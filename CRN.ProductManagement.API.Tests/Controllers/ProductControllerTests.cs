using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CRN.ProductManagement.API.Controllers;
using CRN.ProductManagement.Application.Common;
using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;

namespace CRN.ProductManagement.API.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _service;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _service = new Mock<IProductService>();

        _controller = new ProductController(_service.Object);
    }

    [Fact]
    public async Task GetById_Returns_Ok()
    {
        var response = new ApiResponse<ProductDto>
        {
            Success = true,
            Data = new ProductDto
            {
                Id = 1,
                ProductName = "Laptop"
            }
        };

        _service.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(response);

        var result = await _controller.GetById(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_Returns_NotFound()
    {
        var response = new ApiResponse<ProductDto>
        {
            Success = false
        };

        _service.Setup(x => x.GetByIdAsync(10))
                .ReturnsAsync(response);

        var result = await _controller.GetById(10);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}