using firstTryASPnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace firstTryASPnet.Controllers
{
    public class StaffController : Controller
    {
        private readonly HotelContext _context;

        public StaffController()
        {
            _context = new HotelContext();
        }

        public IActionResult ShowAll()
        {
            var staffList = _context.Staff.ToList();
            return View("ShowAll", staffList);
        }

        public IActionResult ShowDetails(int id)
        {
            var staffModel = _context.Staff.FirstOrDefault(s => s.Id == id);
            if (staffModel == null)
                return NotFound();

            return View("ShowDetails", staffModel);
        }

        public IActionResult AddStaff()
        {
            ViewData["Title"] = "Add Staff";
            return View();
        }

        [HttpPost]
        public IActionResult AddStaff(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return RedirectToAction("ShowAll");
            }

            ViewData["Title"] = "Add Staff";
            return View(staff);
        }
    }
}
