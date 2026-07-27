using CRN.ProductManagement.Application.DTOs;
using System.Security.Claims;

namespace CRN.ProductManagement.Application.Interfaces;

public interface ITokenService
{
    Task<AuthResponse> GenerateTokenAsync(
        string userId,
        string email,
        IList<string> roles);

    Task<string> GenerateRefreshTokenAsync();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}