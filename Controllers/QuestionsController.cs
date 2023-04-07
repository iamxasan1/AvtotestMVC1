using AvtotestMVC.Models;
using AvtotestMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AvtotestMVC.Controllers
{
   
    public class QuestionsController : Controller
    {
        private readonly List<QuestionModel>? _questions;

        public QuestionsController()
        {
            var path = Path.Combine("JsonData", "uzlotin.json");
            var json = System.IO.File.ReadAllText(path);

            _questions = JsonConvert.DeserializeObject<List<QuestionModel>>(json);
        }

        public IActionResult Index()
        {
            
            ViewBag.Questions = _questions;
            return View();
        }

        public IActionResult GetQuestionById(int id, int ticketIndex, int? choiceIndex = null) 
        {
            if (!UserService.IsLogged(HttpContext))
            {
                return RedirectToAction("SignIn", "Users");
            }
            if(ticketIndex != null)
            {
                HttpContext.Response.Cookies.Append("CurrentTicketIndex", ticketIndex.ToString());
            }
            if (HttpContext.Request.Cookies.ContainsKey("CurrentTicketIndex"))
            {
                if (UserService.GetCurrentUser(HttpContext) != null)
                {
                    var index = Convert.ToInt32(HttpContext.Request.Cookies["CurrentTicketIndex"]);


                    if (id > index * 10 + 10)
                    {
                        var correctCount = Convert.ToInt32(HttpContext.Request.Cookies["CorrectAnswerCount"]);

                        HttpContext.Response.Cookies.Delete("CurrentTicketIndex");
                        HttpContext.Response.Cookies.Delete("CorrectAnswerCount");

                        var user = UserService.GetCurrentUser(HttpContext);
                        if (user != null)
                        {
                            user.TicketResults.Add(new TicketResult()
                            {
                                CorrectCount = correctCount,
                                QuestionCount = 10,
                                TicketIndex = index
                            });
                        }

                        return RedirectToAction(nameof(Result), new { ticketIndex = index, correctCount = correctCount });
                    }
                }
                else
                {
                    return RedirectToAction("SignIn", "Users");
                }
            }
            var question = _questions?.FirstOrDefault(x => x.Id == id);

            if(question == null)
            {
                ViewBag.QuestionId = id;
                ViewBag.IsSucces = false;
            }
            else
            {
                ViewBag.Question = question; 
                ViewBag.IsSucces = true;

                ViewBag.IsAnswered = choiceIndex != null;

                if(choiceIndex != null)
                {
                    var answer = question.Choices[(int)choiceIndex].Answer;
                    ViewBag.IsCorrectAnswer = answer;
                    ViewBag.ChoiceIndex = choiceIndex;

                    if (answer)
                    {


                        if (HttpContext.Request.Cookies.ContainsKey("CorrectAnswerCount"))
                        {
                            var index = Convert.ToInt32(HttpContext.Request.Cookies["CorrectAnswerCount"]);
                            HttpContext.Response.Cookies.Append("CorrectAnswerCount", $"{index + 1}");
                        }
                        else
                        {
                            HttpContext.Response.Cookies.Append("CorrectAnswerCount", "1");
                        }
                    }
                }
            }
            return View();
        }
        public IActionResult Result(int ticketIndex, int correctCount)
        {
            ViewBag.TicketIndex = ticketIndex;
            ViewBag.CorrectCount = correctCount;
            return View();
        }
    }
   
}
