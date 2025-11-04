using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Configurations.MenuConfigurations;

public class MenuConfiguration:IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(M => M.Id);

        builder.Property(M=>M.Key)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(M => M.Value)
            .IsRequired()
            .HasMaxLength(600);
    }
}
