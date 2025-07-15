using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venue;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("seats");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new SeatId(id))
            .HasColumnName("seat_id");
        
        builder.Property(v => v.VenueId).HasColumnName("venue_id");

    }
    
    
}

