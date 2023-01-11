using QuizGame.Database.Models.Common;

namespace QuizGame.Database.Models
{
    public class Question : BaseEntity
    {
        public string Tittle { get; set; }
        public List<Answer> Answers { get; set; }

        public string ImageName { get; set; } 
        public string ImageNameInFileSystem { get; set; } 
    }
}
