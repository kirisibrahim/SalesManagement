using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Configurations
{
    public class TasksTaskConfiguration : IEntityTypeConfiguration<TasksTask>
    {
        public void Configure(EntityTypeBuilder<TasksTask> builder)
        {
            builder.HasKey(tt => tt.Id);
            builder.Property(tt => tt.Title)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(tt => tt.Description)
                   .HasMaxLength(500);
            builder.Property(tt => tt.CreatedDate)
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(tt => tt.Durum)
                   .HasConversion<string>()  // Enum değeri string olarak saklanır
                   .IsRequired();

            builder.HasMany(tt => tt.UserTasks)
                   .WithOne(ut => ut.TasksTask)
                   .HasForeignKey(ut => ut.TaskId)
                   // Eğer görevin silinmesiyle ilişkili kayıtların da silinmesini istiyorsanız cascade delete kullanabilirsiniz:
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
