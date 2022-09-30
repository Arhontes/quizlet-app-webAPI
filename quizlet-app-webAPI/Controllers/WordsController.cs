using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Data;
using quizlet_app_webAPI.Models;

namespace quizlet_app_webAPI.Controllers
{
    [ApiController]
    [Route("api/words")]
    [EnableCors("_myAllowSpecificOrigins")]
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
                var words = await dbContext.Words.Where(el => el.WordsModuleId == wordsModule.Id).ToListAsync();
                return Ok(words);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddWord(AddWordRequest addWordRequest)
        {

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
                var currentWordsCount = dbContext.Words.Where(el => el.WordsModuleId == module.Id).Count();

                module.Words.Add(word);
                if (currentWordsCount + 1 < 100)
                {
                    module.Words.Add(word);
                    module.maxWordsCount++;
                }
                else
                {
                    return BadRequest("module is full");
                }

                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return BadRequest();

        }
        [HttpPut]
        public async Task<IActionResult> UpdateWord(UpdateWordRequest updateWordRequest)
        {
            var word = await dbContext.Words.FindAsync(Guid.Parse(updateWordRequest.WordId));
            if (word != null)
            {
                word.Transcription = !string.IsNullOrEmpty(updateWordRequest.Transcription) ? updateWordRequest.Transcription : word.Transcription;
                word.Transcription = !string.IsNullOrEmpty(updateWordRequest.WordImgUrl) ? updateWordRequest.WordImgUrl : word.WordImgUrl;
                word.Transcription = !string.IsNullOrEmpty(updateWordRequest.Definition) ? updateWordRequest.Definition : word.Definition;
                word.Transcription = !string.IsNullOrEmpty(updateWordRequest.Meaning) ? updateWordRequest.Meaning : word.Meaning;

                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWord([FromRoute] Guid id)
        {
            var word = await dbContext.Words.FindAsync(id);
            if (word != null)
            {
                dbContext.Remove(word);
                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return NotFound();
        }

    }
}
