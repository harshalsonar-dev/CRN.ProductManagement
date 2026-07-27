namespace CRN.ProductManagement.Infrastructure.Identity;

public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public bool IsRevoked { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;

    public ApplicationUser ApplicationUser { get; set; } = null!;

    public bool IsActive =>
        !IsRevoked && DateTime.UtcNow < Expires;
}