namespace SalesManagement.Business.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public RoleDto Role { get; set; } = new RoleDto(); // null dönmemesi lazım ondan varsayılan değer atadıkk.
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? RoleName { get; set; }

    }
}
