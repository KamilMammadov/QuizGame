using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizGame.Areas.Client.ViewModels.Home;
using QuizGame.Areas.Client.ViewModels.Question;
using QuizGame.Contracts.File;
using QuizGame.Database;
using QuizGame.Database.Models;
using QuizGame.Services.Abstracts;

namespace QuizGame.Areas.Client.Controllers
{
    [Area("client")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _currentUser;
        private static int _currentCount=7;

        public HomeController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _currentUser=userService;


        }
        [HttpGet("/", Name = "client-home-index")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("question", Name = "client-home-question")]
        public IActionResult Question()
        {

            if (!_currentUser.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }

            _currentCount++;
            var question = _dbContext.Questions.FirstOrDefault(q => q.Id == _currentCount);
            if (question is null)
            {
                return NotFound();
            }
            var newmodel = new ListViewModel
            {
                Id = question.Id,
                Tittle = question.Tittle,
                Answers = _dbContext.Answers.Where(a => a.QuestionId == question.Id)
                .Select(a => new Answer(a.Id, a.Title, a.Status))
                .ToList(),
                TotalPoint=_currentUser.CurrentUser.TotalPoint,
               
            };
            return View(newmodel);
        }
        [HttpGet("answer",Name ="client-home-question-answer")]
        public IActionResult Question(int id)
        {
            
            var answer= _dbContext.Answers.FirstOrDefault(a => a.Id == id);

            if (answer is null) return NotFound();


            var user = _dbContext.Users.FirstOrDefault(c=>c.Id==_currentUser.CurrentUser.Id);
            if (answer.Status)
            {
                
                user.TotalPoint += 5;

                _dbContext.SaveChanges();
            }

            var question = _dbContext.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            if (question is null)
            {
                return NotFound();
            }
            var newmodel = new ListViewModel
            {
                Id = question.Id,
                Tittle = question.Tittle,
                Answers = _dbContext.Answers.Where(a => a.QuestionId == question.Id)
                .Select(a => new Answer(a.Id, a.Title, a.Status))
                .ToList(),
                IsAnswered = true,
                TotalPoint=user.TotalPoint

            };
            
            return View(newmodel);
       }

    }
}
