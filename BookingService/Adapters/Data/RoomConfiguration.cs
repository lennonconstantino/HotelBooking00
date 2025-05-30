using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Data
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.Price)
                .Property(e => e.Currency);
            builder.OwnsOne(e => e.Price)
                .Property(e => e.Value);
        }
    }
}
