namespace quizlet_app_webAPI.Models
{
    public class AddWordRequest
    {
        public string Meaning { get; set; }
        public string Definition { get; set; }
        public string? Transcription { get; set; }
        public string? WordImgUrl { get; set; }
        public string WordsModuleId { get; set; }
    }
}
