using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
{
    public void Configure(EntityTypeBuilder<EventDetails> builder)
    {
        builder.ToTable("seats");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new EventDetailsId(id));

    }
}

