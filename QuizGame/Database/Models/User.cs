using QuizGame.Database.Models.Common;

namespace QuizGame.Database.Models
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int TotalPoint { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
