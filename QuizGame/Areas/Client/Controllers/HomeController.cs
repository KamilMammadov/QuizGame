using Microsoft.AspNetCore.Mvc;
using QuizGame.Areas.Client.ViewModels.Home;
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
            var question = _dbContext.Questions.FirstOrDefault(q => q.Id == 1);
            if (question is null)
            {
                return NotFound();
            }
            var newmodel = new Question
            {
                Id = question.Id,
                Tittle = question.Tittle,
                Answers = _dbContext.Answers.Where(a => a.QuestionId == question.Id)
                .Select(a => new Answer(a.Id, a.Title, a.Status))
                .ToList(),
               
            };
            return View(newmodel);
        }
        [HttpPost("question",Name ="client-home-question")]
        public IActionResult Question(int id)
        {
            var answer = _dbContext.Answers.FirstOrDefault(a => a.Id == id);
            if (answer.Status)
            {
                return Ok();
            }
            return View();
        }

    }
}
