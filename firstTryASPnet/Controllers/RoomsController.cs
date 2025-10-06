using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class RoomsController : Controller
    {
        
        public IActionResult Index()
        {
            ViewData["Title"] = " Rooms";
            return View();
        }
        public IActionResult AddRoom()
        {
            ViewData["Title"] = "AddRoom";
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(int RoomNum)
        {
            ViewData["Title"] = " AddRoom";
            return View();
        }
    }
}
