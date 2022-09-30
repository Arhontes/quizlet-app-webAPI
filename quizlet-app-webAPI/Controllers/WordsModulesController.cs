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
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWordsModule([FromRoute] Guid id)
        {
            var wordsModule = await dbContext.WordsModules.FindAsync(id);
            if (wordsModule==null)
            {
                return NotFound();
            }
            return Ok(wordsModule);
        }


        [HttpPost]
        public async Task<IActionResult> AddWordsModule(AddWordsModuleRequest addWordsModuleRequest)
        {
            var wordsModule = new WordsModule()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Name = addWordsModuleRequest.Name,
            };
            await dbContext.WordsModules.AddAsync(wordsModule);
            await dbContext.SaveChangesAsync();

            return Ok(wordsModule);
            
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWordsModule([FromRoute] Guid id, UpdateWordsModuleRequest updateWordsModuleRequest)
        {
            var wordsModule = await dbContext.WordsModules.FindAsync(id);
            if (wordsModule !=null)
            {
                wordsModule.Words = updateWordsModuleRequest.Words;
                wordsModule.CreateDate = updateWordsModuleRequest.CreateDate;
                wordsModule.Name = updateWordsModuleRequest.Name;

                await dbContext.SaveChangesAsync();

                return Ok(wordsModule);
            }
            return NotFound();
        }
    }
}
