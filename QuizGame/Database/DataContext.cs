using Microsoft.EntityFrameworkCore;
using QuizGame.Database.Models;

namespace QuizGame.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
         : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Answer> Answers { get; set; }
    }
}
