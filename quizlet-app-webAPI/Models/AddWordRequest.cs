using System.ComponentModel.DataAnnotations;

namespace quizlet_app_webAPI.Models
{
    public class AddWordRequest
    {
        [Required]
        public string Meaning { get; set; }
        [Required]
        public string Definition { get; set; }
        public string? Transcription { get; set; }
        public string? WordImgUrl { get; set; }
        [Required]
        public string WordsModuleId { get; set; }
    }
}
