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
        private static int _currentCount=7;

        public HomeController(DataContext dbContext)
        {
            _dbContext = dbContext;
         


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
               
            };
            return View(newmodel);
        }
        [HttpGet("answer",Name ="client-home-question-answer")]
        public IActionResult Question(int id)
        {
            
            var answer= _dbContext.Answers.FirstOrDefault(a => a.Id == id);

            if (answer is null) return NotFound();

            if (answer.Status)
            {

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

            };
            
            return View(newmodel);
        }

    }
}
