using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.TotalAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(i => i.IssueDate)
                   .IsRequired();

            // İlişkiler
            builder.HasMany(i => i.Sales)
                   .WithOne(s => s.Invoice)
                   .HasForeignKey(s => s.InvoiceId);
        }
    }
}
