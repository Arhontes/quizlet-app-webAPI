namespace quizlet_app_webAPI.Models
{
    public class WordsModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Words { get; set; }
        public DateTime createDate { get; set; }
    }
}
