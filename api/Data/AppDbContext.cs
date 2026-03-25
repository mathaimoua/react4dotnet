using Microsoft.EntityFrameworkCore;
using api.Models;

// This is Entity Framework's connection to your database, it's the single place that manages the connection and gives you access to your tables.

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; } // DbSet<Person> is how you tell Entity Framework "I have a table of people." When you write _db.People anywhere in your controllers, this is what you're accessing.

}