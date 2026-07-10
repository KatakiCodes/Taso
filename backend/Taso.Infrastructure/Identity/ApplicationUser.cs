using Microsoft.AspNetCore.Identity;

namespace Taso.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
