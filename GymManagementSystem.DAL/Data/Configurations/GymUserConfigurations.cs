using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.DAL.Data.Configurations;

public class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUsers, new()
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Name)
               .HasColumnType("varchar")
               .HasMaxLength(50);
        builder.Property(x => x.Email)
               .HasColumnType("varchar")
               .HasMaxLength (100);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Phone).IsUnique();

        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint("EmailCheck", ("Email like '_&@_%._%'"));
            tb.HasCheckConstraint("PhoneCheck", "Phone like '010%' or Phone like '011%' orPhone like '012%' orPhone like '015%'");
        });

        builder.OwnsOne(x => x.Address, Address =>
        {
            Address.Property(x => x.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);
            Address.Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);
            Address.Property(x => x.BuildingNumber).HasColumnName("BuildingNumber");
        });
    }
}
