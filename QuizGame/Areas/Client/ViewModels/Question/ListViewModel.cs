using QuizGame.Database.Models;

namespace QuizGame.Areas.Client.ViewModels.Home
{
    public class ListViewModel
    {
        public ListViewModel(int id, string title, bool status)
        {
            Id = id;
            Title = title;
            Status = status;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
