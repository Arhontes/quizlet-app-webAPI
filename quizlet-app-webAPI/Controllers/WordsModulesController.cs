global using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Data;
using quizlet_app_webAPI.Models;

namespace quizlet_app_webAPI.Controllers
{
    [ApiController]
    [Route("api/wordsmodules")]
    [Authorize]
    public class WordsModulesController : Controller
    {
        private readonly WordsModuleAPIDbContext dbContext;
        private readonly IUserService _userService;
        public WordsModulesController(WordsModuleAPIDbContext dbContext, IUserService userService)
        {
            _userService = userService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWordsModules()
        {
            var userId = _userService.GetUserId();
            var modules = await dbContext.WordsModules
                .Where(el => el.UserId.ToString() == userId)
                .ToListAsync();

            return Ok(modules);
        }
        [HttpGet, AllowAnonymous]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetOneWordsModule([FromRoute] Guid id)
        {
            var wordsModule = await dbContext.WordsModules.FindAsync(id);
            if (wordsModule == null)
            {
                return NotFound();
            }
            return Ok(wordsModule);
        }


        [HttpPost]
        public async Task<IActionResult> AddWordsModule(AddWordsModuleRequest addWordsModuleRequest)
        {
            var userId = _userService.GetUserId();
           
            var wordsModule = new WordsModule()
            {
                UserId = Guid.Parse(userId),
                WordsModuleId = Guid.NewGuid(),
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
            if (wordsModule != null)
            {
                wordsModule.Name = updateWordsModuleRequest.Name;

                await dbContext.SaveChangesAsync();

                return Ok(wordsModule);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWordsModule([FromRoute] Guid id)
        {
            var module = await dbContext.WordsModules.FindAsync(id);
            
            if (module != null)
            {
                if (module.UserId.ToString() != _userService.GetUserId())
                {
                    return BadRequest("access denied");
                }
                dbContext.Remove(module);

                var words = await dbContext.Words
                    .Where(el => el.WordsModuleId == module.WordsModuleId)
                    .ToListAsync();

                if (words.Any()) dbContext.Remove(words);

                await dbContext.SaveChangesAsync();
                return Ok(module);
            }
            return NotFound();
        }
    }
}
