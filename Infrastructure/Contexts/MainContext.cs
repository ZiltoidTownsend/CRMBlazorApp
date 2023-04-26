using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class MainContext : MainAbstructContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }
        public DbSet<Contact> Contacts { get; set; }
}
