using QuizGame.Database.Models;

namespace QuizGame.Areas.Client.ViewModels.Question
{
	public class ListViewModel
	{
        public int Id { get; set; }
        public string Tittle { get; set; }
        public List<Answer> Answers { get; set; }
        public bool IsAnswered { get; set; }
        public int TotalPoint { get; set; }

    }
}
