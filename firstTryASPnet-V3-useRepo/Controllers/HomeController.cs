using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        } 
    }
}