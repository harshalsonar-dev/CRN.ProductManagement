using Microsoft.AspNetCore.Identity;

namespace CRN.ProductManagement.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}