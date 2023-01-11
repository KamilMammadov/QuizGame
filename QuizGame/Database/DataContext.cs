using Microsoft.EntityFrameworkCore;

namespace QuizGame.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
         : base(options)
        {

        }
    }
}
