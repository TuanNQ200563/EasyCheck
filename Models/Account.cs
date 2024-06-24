namespace EasyCheck.Models
{
    public enum Role
    {
        ADMIN = 1,
        TEACHER = 2,
        PARENT = 3
    }
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}