using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // ilişkiler
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId);

            // dizinler
            builder.HasIndex(c => c.Name).IsUnique();  // kategori ismi uniq olmalı
        }
    }
}
