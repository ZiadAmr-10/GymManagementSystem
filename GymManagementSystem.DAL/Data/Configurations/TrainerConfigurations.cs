using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace GymManagementSystem.DAL.Data.Configurations;

public class TrainerConfigurations : GymUserConfigurations<Trainer>
{
    public override void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.Property(x => x.CreatedAt)
                .HasColumnName("HiringDate")
                .HasDefaultValueSql("GETDATE()");

        base.Configure(builder);
    }
}
