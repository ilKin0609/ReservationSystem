using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;
using ReservationSys.Domain.Enums;

namespace ReservationSys.Persistence.Configurations.ReservationConfigurations;

public class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(Res=>Res.Id);

        builder.Property(Res => Res.StartTime)
               .IsRequired();

        builder.Property(Res => Res.EndTime)
               .IsRequired();

        
        builder.Property(Res => Res.ReservationStatus)
               .HasConversion<int>()
               .HasDefaultValue(ReservationStatus.Pending);


        builder.HasOne(Res=>Res.AppUser)
            .WithMany(U=>U.Reservations)
            .HasForeignKey(Res=>Res.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(Res => Res.Restaurant)
            .WithMany(Rt => Rt.Reservations)
            .HasForeignKey(Res => Res.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(Res => Res.Table)
            .WithMany(T => T.Reservations)
            .HasForeignKey(Res => Res.TableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
