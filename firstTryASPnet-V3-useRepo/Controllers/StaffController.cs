using firstTryASPnet.Models;
using firstTryASPnet.Repositories;
using firstTryASPnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffService _staffService;

        public StaffController()
        {
            var context = new HotelContext();
            var repo = new StaffRepository(context);
            _staffService = new StaffService(repo);
        }

        public IActionResult ShowAll()
        {
            var staffList = _staffService.GetAllStaff();
            return View("ShowAll", staffList);
        }

        public IActionResult ShowDetails(int id)
        {
            var staffModel = _staffService.GetStaffById(id);
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
                _staffService.AddStaff(staff);
                return RedirectToAction("ShowAll");
            }

            ViewData["Title"] = "Add Staff";
            return View(staff);
        }
    }
}
