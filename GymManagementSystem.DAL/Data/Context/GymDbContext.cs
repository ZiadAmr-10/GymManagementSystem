using GymManagementSystem.DAL.Data.Configurations;
using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagementSystem.DAL.Data.Context;

public class GymDbContext :DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options):base(options)
    {
        
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()) ;
    }

    public DbSet<Plan> Plans { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Trainer> Trainer { get; set; }
}
