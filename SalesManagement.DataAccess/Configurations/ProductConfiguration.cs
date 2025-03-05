using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            // İlişkiler
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);  // dikkat!! kategoriyi silersek kategoriye ait ürünler de silinir, delete işlemini admin yapabilir...

            // dizinler
            builder.HasIndex(p => p.Name).IsUnique();  // ürün adı benzersiz olmalı
        }
    }
}
