using System.Text.Json.Serialization;

namespace quizlet_app_webAPI.Models
{
    public class WordsModule
    {
        protected internal int maxWordsCount = 100;
        public WordsModule()
        {
            Words = new List<Word>();   
        }
        private string name;
        [Key]
        public Guid WordsModuleId { get; set; }
        public string Name
        {
            get => name;
            set => name = (!string.IsNullOrWhiteSpace(value)) ? value : "word";
        }
        public List<Word> Words { get; set; }

        public int WordsCount { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
