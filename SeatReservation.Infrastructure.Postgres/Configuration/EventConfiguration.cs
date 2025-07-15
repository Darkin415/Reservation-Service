using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venue;
using SeatReservation.Infrastructure.Postgres.Converters;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {

        builder.ToTable("events");

        builder.HasKey(e => e.Id);

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new EventId(id))
            .HasColumnName("events_id");

        builder.HasOne<Venue>().WithMany().HasForeignKey(v => v.VenueId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.VenueId).HasColumnName("Venue_id");

        builder.Property(e => e.Type)
            .HasConversion<string>();

        builder.Property(e => e.Info)
            .HasConversion(new EventInfoConverter());




    }
}

