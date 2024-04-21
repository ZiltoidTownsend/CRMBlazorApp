using Domain.Entities;
using Infrastructure.Models.Profile;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class MainContext : MainAbstructContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}