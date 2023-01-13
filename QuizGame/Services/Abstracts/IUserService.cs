using QuizGame.Areas.Client.ViewModels.Authentication;
using QuizGame.Database.Models;

namespace QuizGame.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public User CurrentUser { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);
        string GetCurrentUserFullName();
        Task SignInAsync(int id);
        Task SignInAsync(string? email, string? password);
        Task CreateAsync(RegisterViewModel model);
        Task SignOutAsync();
      
    }
}
