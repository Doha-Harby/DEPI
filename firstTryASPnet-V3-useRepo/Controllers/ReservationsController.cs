using firstTryASPnet.Models;
using firstTryASPnet.Services;
using firstTryASPnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ReservationService _reservationService;

        public ReservationsController()
        {
            var context = new HotelContext();
            var repo = new ReservationRepository(context);
            _reservationService = new ReservationService(repo);
        }

        // عرض كل الحجوزات
        public IActionResult ShowAll()
        {
            var reservations = _reservationService.GetAllReservations();
            return View("ShowAll", reservations);
        }

        // عرض تفاصيل حجز معين
        public IActionResult ShowDetails(int id)
        {
            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
                return NotFound();

            return View("ShowDetails", reservation);
        }

        // عرض فورم الحجز
        public IActionResult AddReservation()
        {
            return View();
        }

        // استلام بيانات الحجز
        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _reservationService.AddReservation(reservation);
                return RedirectToAction("ShowAll");
            }

            return View(reservation);
        }
    }
}
