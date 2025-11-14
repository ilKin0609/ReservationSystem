using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Configurations.RestaurantConfigurations;

public class RestaurantConfiguration:IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(Rt => Rt.Id);


        builder.Property(r => r.Slug)
            .HasMaxLength(200);

        builder.HasIndex(r => r.Slug)
            .IsUnique();

        builder.Property(Rt => Rt.Name)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(Rt => Rt.Latitude)
              .HasColumnType("decimal(9,6)");

        builder.Property(Rt => Rt.Longitude)
               .HasColumnType("decimal(9,6)");
       
        builder.HasOne(r => r.Category)
               .WithMany(c => c.Restaurants)
               .HasForeignKey(r => r.CategoryId);
        
        builder.HasOne(Rt=>Rt.User)
            .WithMany(U=>U.Restaurants)
            .HasForeignKey(Rt=>Rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(Rt=>Rt.Tables)
            .WithOne(T=>T.Restaurant)
            .HasForeignKey(T=>T.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
