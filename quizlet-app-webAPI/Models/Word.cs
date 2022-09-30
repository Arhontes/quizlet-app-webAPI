namespace quizlet_app_webAPI.Models
{
    public class Word
    {

        private string _meaning;
        private string _definition;
        
        public Guid WordId { get; set; }
        public string Meaning
        {
            get => _meaning;
            set => _meaning = (!string.IsNullOrWhiteSpace(value)) ? value : "word";
        }
        public string Definition
        {
            get => _definition;
            set => _definition = (!string.IsNullOrWhiteSpace(value)) ? value : "word translation";
        }
        public string? Transcription { get; set; }
        public string? WordImgUrl { get; set; }
        public Guid WordsModuleId { get; set; }
       
    }
}
