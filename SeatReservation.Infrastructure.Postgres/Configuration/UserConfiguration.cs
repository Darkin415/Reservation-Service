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
            .HasColumnName("user_id");

        builder.OwnsOne(u => u.Details, db =>
        {
            db.ToJson("details");
            db.OwnsMany(d => d.Social, sb =>
            {
                sb.Property(u => u.Name).IsRequired().HasMaxLength(ConstantsLength.LENGTH500).HasColumnName("name");
                sb.Property(u => u.Link).IsRequired().HasMaxLength(ConstantsLength.LENGTH500).HasColumnName("link");
            });
            
            db.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(ConstantsLength.LENGTH500)
                .HasColumnName("description");

        });



    }
}