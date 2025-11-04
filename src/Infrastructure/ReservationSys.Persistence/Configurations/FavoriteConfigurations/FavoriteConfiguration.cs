using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Configurations.FavoriteConfigurations;

public class FavoriteConfiguration:IEntityTypeConfiguration<Favourite>
{
    public void Configure(EntityTypeBuilder<Favourite> builder)
    {
        builder.HasKey(F => F.Id);

        builder.HasOne(F => F.User)
            .WithMany(U => U.Favourites)
            .HasForeignKey(F => F.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(F => F.Restaurant)
            .WithMany(R => R.Favourites)
            .HasForeignKey(F => F.RestuarantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

