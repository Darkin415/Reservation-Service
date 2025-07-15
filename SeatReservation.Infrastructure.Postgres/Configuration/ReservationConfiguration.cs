using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Reservation;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new ReservationId(id))
            .HasColumnName("reservation_id");
        
        builder.Property(v => v.EventId)
            .HasConversion(v => v.Value, id => new EventId(id))
            .HasColumnName("event_id");
        
      

    }
}

