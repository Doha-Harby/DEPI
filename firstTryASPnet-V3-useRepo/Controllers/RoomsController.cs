using firstTryASPnet.Models;
using firstTryASPnet.Services;
using firstTryASPnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomService _roomService;

        public RoomsController()
        {
            var context = new HotelContext();
            var repo = new RoomRepository(context);
            _roomService = new RoomService(repo);
        }

        public IActionResult ShowAll()
        {
            var rooms = _roomService.GetAllRooms();
            return View("ShowAll", rooms);
        }

        public IActionResult ShowDetails(int id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
                return NotFound();

            return View("ShowDetails", room);
        }

        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            if (ModelState.IsValid)
            {
                _roomService.AddRoom(room);
                return RedirectToAction("ShowAll");
            }

            return View(room);
        }
    }
}
