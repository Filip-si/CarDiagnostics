using CarDiagnostics.API.src.Modules.User.Data.Configurations;
using CarDiagnostics.API.src.Modules.User.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDiagnostics.API.src.Modules.User.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}
