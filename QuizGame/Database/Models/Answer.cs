using QuizGame.Database.Models.Common;

namespace QuizGame.Database.Models
{
    public class Answer : BaseEntity
    {
        public Answer(int id,string title, bool status)
        {
            Id = id;
            Title = title;
            Status = status;
        }

        public string Title { get; set; }
        public bool Status { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
