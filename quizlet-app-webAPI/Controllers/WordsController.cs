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
    [Authorize]
    public class WordsController : Controller
    {
        private readonly WordsModuleAPIDbContext dbContext;
        private readonly IUserService _userService;
        public WordsController(WordsModuleAPIDbContext dbContext, IUserService userService)
        {
            _userService = userService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWords([FromRoute] Guid id)
        {
            var wordsList = await dbContext.Words.Where(x => x.WordsModuleId == id).ToListAsync();
            if(wordsList.Count == 0)
            {
                return NotFound();
            }
            return Ok(wordsList);
        }

        [HttpPost]
        public async Task<IActionResult> AddWord(AddWordRequest addWordRequest)
        {

            var userId = _userService.GetUserId();

            var module = await dbContext.WordsModules.FindAsync(Guid.Parse(addWordRequest.WordsModuleId));

            if (module != null)
            {
                if (module.UserId.ToString() != userId)
                {
                    return BadRequest("access denied");
                }

                var currentWordsCount = dbContext.Words.Where(el => el.WordsModuleId == module.WordsModuleId).Count();

                if (currentWordsCount >= 100)
                {
                    return BadRequest("module is full");
                }
                var word = new Word()
                {
                    Definition = addWordRequest.Definition,
                    Meaning = addWordRequest.Meaning,
                    Transcription = addWordRequest.Transcription,
                    WordImgUrl = addWordRequest.WordImgUrl,
                    WordId = new Guid(),
                    WordsModuleId = module.WordsModuleId,
                    WordsModule = module,
                    
                };

                await dbContext.Words.AddAsync(word);

                module.WordsCount++;
                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return NotFound("module not found");

        }

        [HttpPut]
        public async Task<IActionResult> UpdateWord(UpdateWordRequest updateWordRequest)
        {
            var userId = _userService.GetUserId();

            

            var word = await dbContext.Words.FindAsync(Guid.Parse(updateWordRequest.WordId));

            if ( word != null)
            {

                var module = await dbContext.WordsModules.FindAsync(word.WordsModuleId);
                if (module == null)
                {
                    return NotFound("module not found");
                }
                else if (module.UserId.ToString() != userId)
                {
                    return BadRequest("access denied");
                }

                word.Transcription = !string.IsNullOrEmpty(updateWordRequest.Transcription) ? updateWordRequest.Transcription : word.Transcription;
                word.WordImgUrl = !string.IsNullOrEmpty(updateWordRequest.WordImgUrl) ? updateWordRequest.WordImgUrl : word.WordImgUrl;
                word.Definition = !string.IsNullOrEmpty(updateWordRequest.Definition) ? updateWordRequest.Definition : word.Definition;
                word.Meaning = !string.IsNullOrEmpty(updateWordRequest.Meaning) ? updateWordRequest.Meaning : word.Meaning;

                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWord([FromRoute] Guid id)
        {

            var userId = _userService.GetUserId();

            var word = await dbContext.Words.FindAsync(id);
            if (word != null)
            {
                var module = await dbContext.WordsModules.FindAsync(word.WordsModuleId);
                if (module == null)
                {
                    return NotFound("module not found");
                }
                else if (module.UserId.ToString() != userId)
                {
                    return BadRequest("access denied");
                }

                dbContext.Remove(word);
                module.WordsCount --;
                await dbContext.SaveChangesAsync();

                return Ok(word);
            }
            return NotFound();
        }

    }
}
