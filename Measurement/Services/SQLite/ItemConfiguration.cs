using Measurement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Measurement.Services.SQLite
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne<Order>(i => i.Order)
                .WithMany(o => o.Items);
                   //.HasForeignKey(i => i.OrderID)
                   //.IsRequired();
            
            builder.HasIndex(i => new { i.OrderID, i.Number }).IsUnique();
        }
    }
}