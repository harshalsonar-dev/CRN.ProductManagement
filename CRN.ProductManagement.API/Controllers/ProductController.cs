using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CRN.ProductManagement.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetAll(
    [FromQuery] ProductQueryParameters queryParameters)
    {
        var result = await _productService.GetProductsAsync(queryParameters);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var result = await _productService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById),
            new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }
}