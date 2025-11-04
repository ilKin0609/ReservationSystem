using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSys.Domain.Entities;

namespace ReservationSys.Persistence.Configurations.TableConfiguration;

public class TableConfiguration:IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(T=>T.Id);

        builder.Property(t => t.TableNumber)
               .IsRequired();

       
        builder.Property(t => t.Capacity)
               .IsRequired();

        
        builder.Property(t => t.IsAvailable)
               .HasDefaultValue(true);
    }
}
