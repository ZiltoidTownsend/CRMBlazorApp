using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Identity;
public class CRMUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
}
