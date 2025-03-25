using System.ComponentModel.DataAnnotations;

namespace SalesManagement.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Role Role { get; set; }
        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();

    }
}
