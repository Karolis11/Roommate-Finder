using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using roommate_app.Models;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Listing> Listings { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Reply> Replies { get; set; }
}