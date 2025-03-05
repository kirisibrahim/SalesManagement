using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(sm => sm.Id);
            builder.Property(sm => sm.Quantity)
                   .IsRequired();
            builder.Property(sm => sm.MovementType)
                   .HasMaxLength(50);

            // Relationships
            builder.HasOne(sm => sm.Product)
                   .WithMany(p => p.StockMovements)
                   .HasForeignKey(sm => sm.ProductId);
        }
    }
}
