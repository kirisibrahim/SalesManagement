using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            // Relationships
            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId);
        }
    }
}
