using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = " All taff";
            return View();
        }

        public IActionResult AddStaff()
        {
            ViewData["Title"] = " Add Staff";
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(int staffID)
        {
            ViewData["Title"] = " Add Staff";
            return View();
        }

    }
}
