using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Configurations.DetailsRestaurantConfigurations;

public class DetailsRestaurantConfiguration:IEntityTypeConfiguration<DetailsRestaurant>
{
    public void Configure(EntityTypeBuilder<DetailsRestaurant> builder)
    {
        builder.HasKey(Dr => Dr.Id);

        builder.Property(Dr => Dr.Adress)
            .IsRequired()
            .HasMaxLength(2500);

        builder.Property(Dr => Dr.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(Dr => Dr.Email)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(Dr => Dr.Description)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        
        builder.HasOne(Dr => Dr.Restaurant)
            .WithOne(R => R.DetailsRestaurant)
            .HasForeignKey<DetailsRestaurant>(Dr => Dr.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.HasOne(Dr => Dr.Menu)
            .WithOne(M => M.DetailsRestaurant)
            .HasForeignKey<Menu>(M => M.DetailsRestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
