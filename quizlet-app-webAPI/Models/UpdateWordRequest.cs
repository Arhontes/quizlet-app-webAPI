using System.ComponentModel.DataAnnotations;

namespace quizlet_app_webAPI.Models
{
    public class UpdateWordRequest
    {
        [Required]
        public string WordId { get; set; }
        public string? Meaning { get; set; }
        public string? Definition { get; set; }
        public string? Transcription { get; set; }
        public string? WordImgUrl { get; set; }
        
    }
}
