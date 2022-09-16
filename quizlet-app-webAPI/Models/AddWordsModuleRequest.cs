namespace quizlet_app_webAPI.Models
{
    public class AddWordsModuleRequest
    {
        public string Name { get; set; }
        public string Words { get; set; }
        public DateTime createDate { get; set; }
    }
}
