using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Configurations
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasKey(ut => new { ut.TaskId, ut.UserId });

            builder.HasOne(ut => ut.TasksTask)
                   .WithMany(tt => tt.UserTasks)
                   .HasForeignKey(ut => ut.TaskId);

            builder.HasOne(ut => ut.User)
                   .WithMany(u => u.UserTasks)
                   .HasForeignKey(ut => ut.UserId);
        }
    }
}
