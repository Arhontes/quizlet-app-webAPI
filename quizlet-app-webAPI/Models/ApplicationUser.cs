namespace quizlet_app_webAPI.Models
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
      
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
