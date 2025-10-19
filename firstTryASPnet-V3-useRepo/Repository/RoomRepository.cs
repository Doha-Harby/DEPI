using firstTryASPnet.Models;
using System.Collections.Generic;
using System.Linq;

namespace firstTryASPnet.Repositories
{
    public class RoomRepository
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        public List<Room> GetAll()
        {
            return _context.Room.ToList();
        }

        public Room GetById(int id)
        {
            return _context.Room.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Room room)
        {
            _context.Room.Add(room);
            _context.SaveChanges();
        }
    }
}
