﻿namespace quizlet_app_webAPI.Models
{
    public class UpdateWordsModuleRequest
    {
        public string Name { get; set; }
        public List<Word> Words { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
