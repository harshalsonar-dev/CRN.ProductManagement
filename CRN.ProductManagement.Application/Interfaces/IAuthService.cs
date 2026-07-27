using CRN.ProductManagement.Application.DTOs;

namespace CRN.ProductManagement.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request);

    Task<AuthResponse?> LoginAsync(LoginRequest request);

    Task<AuthResponse?> RefreshTokenAsync(RefreshTokenRequest request);

    Task<bool> LogoutAsync(LogoutRequest request);
}