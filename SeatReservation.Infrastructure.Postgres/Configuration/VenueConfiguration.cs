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

        builder.HasKey(v => v.Id).HasName("pk_venues");

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new VenueId(id))
            .HasColumnName("venue_id");

        builder.ComplexProperty(v => v.VenueName, nb =>
        {
            nb.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(ConstantsLength.LENGTH50)
                .HasColumnName("name");
            
            nb.Property(v =>v.Prefix)
                .IsRequired()
                .HasMaxLength(ConstantsLength.LENGTH50)
                .HasColumnName("prefix");
        });

        builder.HasMany(v => v.Seats)
            .WithOne()
            .HasForeignKey(s => s.VenueId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);;

    }
}

