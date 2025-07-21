using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venues;

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

        builder.Property(s => s.RowNumber)
            .HasColumnName("row_number");

        builder.Property(s => s.SeatNumber)
            .HasColumnName("seat_number");

        builder.Property(s => s.VenueId)
            .HasConversion(v => v.Value, id => new VenueId(id))
            .HasColumnName("venue_id");

    }
    
    
}

