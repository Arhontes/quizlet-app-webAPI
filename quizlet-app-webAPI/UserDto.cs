using System.ComponentModel.DataAnnotations;

namespace quizlet_app_webAPI
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


    }
}
