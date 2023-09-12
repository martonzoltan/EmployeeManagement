using Microsoft.EntityFrameworkCore;
using Shared.Models.Entities;

namespace API.Context;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}