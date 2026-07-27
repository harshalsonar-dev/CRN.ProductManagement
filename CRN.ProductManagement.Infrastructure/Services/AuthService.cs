using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Infrastructure.Data;
using CRN.ProductManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace CRN.ProductManagement.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
            return false;

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return false;

        // Assign Default Role
        await _userManager.AddToRoleAsync(user, "User");

        return true;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return null;

        var validPassword =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!validPassword)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        var authResponse =
            await _tokenService.GenerateTokenAsync(
                user.Id,
                user.Email!,
                roles);

        var refreshToken = new RefreshToken
        {
            Token = authResponse.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            ApplicationUserId = user.Id
        };

        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();

        return authResponse;
    }

    public async Task<AuthResponse?> RefreshTokenAsync(
        RefreshTokenRequest request)
    {
        var principal =
            _tokenService.GetPrincipalFromExpiredToken(request.Token);

        var email =
            principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        if (string.IsNullOrWhiteSpace(email))
            return null;

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var storedRefreshToken =
            await _context.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.Token == request.RefreshToken &&
                    x.ApplicationUserId == user.Id);

        if (storedRefreshToken == null)
            return null;

        if (!storedRefreshToken.IsActive)
            return null;

        // Revoke old refresh token
        storedRefreshToken.IsRevoked = true;

        // Get User Roles
        var roles = await _userManager.GetRolesAsync(user);

        // Generate New JWT + Refresh Token
        var authResponse =
            await _tokenService.GenerateTokenAsync(
                user.Id,
                user.Email!,
                roles);

        var newRefreshToken = new RefreshToken
        {
            Token = authResponse.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            ApplicationUserId = user.Id
        };

        _context.RefreshTokens.Add(newRefreshToken);

        await _context.SaveChangesAsync();

        return authResponse;
    }

    public async Task<bool> LogoutAsync(
        LogoutRequest request)
    {
        var token =
            await _context.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.Token == request.RefreshToken);

        if (token == null)
            return false;

        token.IsRevoked = true;

        await _context.SaveChangesAsync();

        return true;
    }
}