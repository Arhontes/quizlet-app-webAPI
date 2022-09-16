using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Data;
using quizlet_app_webAPI.Models;

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
        public async Task<IActionResult> GetWordsModules()
        {
            return Ok(await dbContext.WordsModules.ToListAsync());
            
        }
        [HttpPost]
        public async Task<IActionResult> AddWordsModule(AddWordsModuleRequest addWordsModuleRequest)
        {
            var wordsModule = new WordsModule()
            {
                Id = Guid.NewGuid(),
                Words = addWordsModuleRequest.Words,
                CreateDate = DateTime.Now,
                Name = addWordsModuleRequest.Name,
            };
            await dbContext.WordsModules.AddAsync(wordsModule);
            await dbContext.SaveChangesAsync();

            return Ok(wordsModule);
            
        }
    }
}
