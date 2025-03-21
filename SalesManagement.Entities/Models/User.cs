namespace SalesManagement.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; }
        public ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();

    }
}
