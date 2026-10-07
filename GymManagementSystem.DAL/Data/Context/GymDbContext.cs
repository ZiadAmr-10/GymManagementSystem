using GymManagementSystem.DAL.Data.Configurations;
using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Data.Context;

public class GymDbContext :DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=GymManagementSystem;Trusted_Connection=True;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlanConfigurations());
    }
    public DbSet<Plan> Plans { get; set; }
}
