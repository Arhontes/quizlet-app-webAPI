namespace quizlet_app_webAPI.Models
{
    public class WordsModule
    {
        public WordsModule()
        {
            Words = new List<Word>();
        }
        private string name;
        public Guid Id { get; set; }
        public string Name
        {
            get => name;
            set => name = (!string.IsNullOrWhiteSpace(value)) ? value : "word";
        }
        public List<Word> Words { get; set; }
        
      
        public DateTime CreateDate { get; set; }
    }
}
