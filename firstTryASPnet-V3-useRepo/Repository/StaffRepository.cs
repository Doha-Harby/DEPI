using firstTryASPnet.Models;
using System.Collections.Generic;
using System.Linq;

namespace firstTryASPnet.Repositories
{
    public class StaffRepository
    {
        private readonly HotelContext _context;

        public StaffRepository(HotelContext context)
        {
            _context = context;
        }

        public List<Staff> GetAll()
        {
            return _context.Staff.ToList();
        }

        public Staff GetById(int id)
        {
            return _context.Staff.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Staff staff)
        {
            _context.Staff.Add(staff);
            _context.SaveChanges();
        }
    }
}
