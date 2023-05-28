using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class MainContext : MainAbstructContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}