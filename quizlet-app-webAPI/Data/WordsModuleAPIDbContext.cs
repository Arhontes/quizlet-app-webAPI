using Microsoft.EntityFrameworkCore;
using quizlet_app_webAPI.Models;

namespace quizlet_app_webAPI.Data
{
    public class WordsModuleAPIDbContext : DbContext
    {
        public WordsModuleAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<WordsModule> WordsModules { get; set; }
    }
}
