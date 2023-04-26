using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;
public abstract class MainAbstructContext : IdentityDbContext<CRMUser>
{
    protected MainAbstructContext(DbContextOptions options) : base(options) { }
}
