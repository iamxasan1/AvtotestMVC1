using AvtotestMVC.Models;
using AvtotestMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AvtotestMVC.Controllers
{

    public class TicketsController : Controller
    {
        List<User> users = UserService.Users;
        private readonly List<QuestionModel>? _questions;

        public TicketsController()
        {
            var path = Path.Combine("JsonData", "uzlotin.json");
            var json = System.IO.File.ReadAllText(path);

            _questions = JsonConvert.DeserializeObject<List<QuestionModel>>(json);
            
        }
        public IActionResult Index()
        {
            ViewBag.TicketsCount = _questions?.Count / 10;

            return View();
        }
    }
}
