using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.TotalPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(s => s.SaleDate)
                   .IsRequired();

            // Relationships
            builder.HasOne(s => s.Product)
                   .WithMany(p => p.Sales) 
                   .HasForeignKey(s => s.ProductId);
        }
    }
}
