using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Data;
using quizlet_app_webAPI.Models;

namespace quizlet_app_webAPI.Controllers
{   [ApiController]
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly WordsModuleAPIDbContext dbContext;

        public WordsController(WordsModuleAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<IActionResult> GetWords([FromRoute] Guid id)
        {
            var wordsModule = await dbContext.WordsModules.FindAsync(id);
            if (wordsModule != null)
            {
                var words = await dbContext.Words.Where(el=>el.WordsModuleId==wordsModule.Id).ToListAsync();
                return Ok(words);
            } 
            else return BadRequest();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddWords(AddWordRequest addWordRequest)
        {

            if (addWordRequest.WordsModuleId != null || addWordRequest.WordsModuleId.Equals("")) {
                var module = await dbContext.WordsModules.FindAsync(Guid.Parse(addWordRequest.WordsModuleId));
                if (module != null)
                {
                    var word = new Word()
                    {
                        Definition = addWordRequest.Definition,
                        Meaning = addWordRequest.Meaning,
                        Transcription = addWordRequest.Transcription,
                        WordImgUrl = addWordRequest.WordImgUrl,
                        WordId = new Guid(),
                        WordsModuleId = module.Id
                    };
                    module.Words.Add(word);
                    
                    await dbContext.SaveChangesAsync();

                    return Ok(word);
                }
                else return BadRequest("module not found");
                
            }
            else return BadRequest();
              
        }

       
    }
}
