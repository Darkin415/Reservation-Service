using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
{
    public void Configure(EntityTypeBuilder<EventDetails> builder)
    {
        builder.ToTable("event_details");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new EventDetailsId(id))
            .HasColumnName("event_details_id");
        
        builder.Property(v => v.EventId)
            .HasConversion(v => v.Value, id => new EventId(id))
            .HasColumnName("event_id");   
        
        builder.HasOne<Event>().WithOne(e => e.Details).HasForeignKey<EventDetails>(d => d.EventId);

    }
}

