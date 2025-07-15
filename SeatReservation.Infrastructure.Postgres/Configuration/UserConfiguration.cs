using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasColumnName("reservation_seats_id");

        builder.OwnsMany(u => u.Socials, sb =>
        {
            sb.ToJson("social");
            sb.Property(u => u.Link)
                .IsRequired()
                .HasMaxLength(ConstantsLength.LENGTH500)
                .HasColumnName("link");
            sb.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(ConstantsLength.LENGTH500)
                .HasColumnName("name");
        });



    }
}