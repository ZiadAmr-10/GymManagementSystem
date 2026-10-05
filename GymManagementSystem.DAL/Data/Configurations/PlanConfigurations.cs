using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.DAL.Data.Configurations;

public class PlanConfigurations : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

        builder.Property(x => x.Description)
                .HasMaxLength(200);

        builder.Property(x => x.Price)
                .HasPrecision(10, 2);

        builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

        builder.ToTable(Tb =>
        {
            Tb.HasCheckConstraint("PlanDurationChecl", "DurationDays Between 1 and 365");
        });

    }
}
