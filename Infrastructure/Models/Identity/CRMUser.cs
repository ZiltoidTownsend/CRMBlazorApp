using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Identity;
public class CRMUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
