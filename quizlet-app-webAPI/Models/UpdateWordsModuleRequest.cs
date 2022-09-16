namespace quizlet_app_webAPI.Models
{
    public class UpdateWordsModuleRequest
    {
        public string Name { get; set; }
        public string Words { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
