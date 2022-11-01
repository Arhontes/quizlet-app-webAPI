global using System.ComponentModel.DataAnnotations;

namespace quizlet_app_webAPI.Models
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            WordsModules = new List<WordsModule>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<WordsModule> WordsModules { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
