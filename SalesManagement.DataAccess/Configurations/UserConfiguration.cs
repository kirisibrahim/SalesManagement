using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagement.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            // İlişkiler
            builder.HasOne(u => u.Role)  // Her kullanıcın bir rolü var
                   .WithMany(r => r.Users)  // rollerin birden fazla kullanıcı olabilir
                   .HasForeignKey(u => u.RoleId);
        }
    }
}
