using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;
using SeatReservation.Domain.Venue;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("venues");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new VenueId(id));    

        builder.OwnsOne(v => v.VenueName, nb =>
        {
            nb.Property(v => v.Prefix)          
            .HasMaxLength(ConstantsLength.LENGTH50)
            .HasColumnName("prefix");

            nb.Property(v => v.Name)          
           .HasMaxLength(ConstantsLength.LENGTH500)
           .HasColumnName("name");

        });

        builder.Navigation(v => v.VenueName).IsRequired(false);

    }
}

