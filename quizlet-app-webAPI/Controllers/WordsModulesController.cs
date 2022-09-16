using Microsoft.AspNetCore.Mvc;
using quizlet_app_webAPI.Data;

namespace quizlet_app_webAPI.Controllers
{   [ApiController]
    [Route("api/wordsmodules")]
    public class WordsModulesController : Controller
    {
        private readonly WordsModuleAPIDbContext dbContext;

        public WordsModulesController(WordsModuleAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetWordsModules()
        {
            return Ok(dbContext.WordsModules.ToList());
            
        }
    }
}
