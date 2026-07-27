using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRN.ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result)
        {
            return BadRequest(new
            {
                Message = "User already exists."
            });
        }

        return Ok(new
        {
            Message = "User registered successfully."
        });
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response == null)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password."
            });
        }

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(
    RefreshTokenRequest request)
    {
        var response =
            await _authService.RefreshTokenAsync(request);

        if (response == null)
            return Unauthorized();

        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        LogoutRequest request)
    {
        var result =
            await _authService.LogoutAsync(request);

        if (!result)
            return BadRequest();

        return Ok(new
        {
            Message = "Logged out successfully."
        });
    }
}