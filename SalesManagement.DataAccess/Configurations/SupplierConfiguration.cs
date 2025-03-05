using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace SalesManagement.DataAccess.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.ContactInfo)
                   .HasMaxLength(500);

            // Relationships
            builder.HasMany(s => s.Products)
                   .WithOne(p => p.Supplier)
                   .HasForeignKey(p => p.SupplierId);
        }
    }
}
