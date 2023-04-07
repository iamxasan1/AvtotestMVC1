using Microsoft.AspNetCore.Mvc;

namespace AvtotestMVC.Controllers
{
    public class HttpMethods : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
