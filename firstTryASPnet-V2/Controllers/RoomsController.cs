using firstTryASPnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace firstTryASPnet.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelContext _context;

        public RoomsController()
        {
            _context = new HotelContext();
        }

        public IActionResult ShowAll()
        {
            var roomList = _context.Room.ToList();
            return View("ShowAll", roomList);
        }

        public IActionResult ShowDetails(int id)
        {
            var roomModel = _context.Room.FirstOrDefault(r => r.Id == id);
            if (roomModel == null)
                return NotFound();

            return View("ShowDetails", roomModel);
        }

        public IActionResult AddRoom()
        {
            ViewData["Title"] = "Add Room";
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Room.Add(room);
                _context.SaveChanges();
                return RedirectToAction("ShowAll");
            }

            ViewData["Title"] = "Add Room";
            return View(room);
        }
    }
}
