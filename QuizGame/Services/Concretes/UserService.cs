using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Bcpg;
using QuizGame.Areas.Client.ViewModels.Authentication;
using QuizGame.Database;
using System.Security.Claims;
using System.Text.Json;
using QuizGame.Contracts.Identity;
using QuizGame.Services.Abstracts;
using QuizGame.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizGame.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;

        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new Exception("error");

                _currentUser = _dataContext.Users.First(u => u.Id == Int32.Parse(idClaim.Value));

                return _currentUser;
            }
        }

     


        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName} {CurrentUser.TotalPoint}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.Password == password);
        }

        public async Task SignInAsync(int id)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email && u.Password == password);
            await SignInAsync(user.Id);
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUserAsync();



            await _dataContext.SaveChangesAsync();


            async Task<User> CreateUserAsync()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }
        }
    }
}
