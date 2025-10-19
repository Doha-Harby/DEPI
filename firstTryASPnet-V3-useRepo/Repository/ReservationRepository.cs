using firstTryASPnet.Models;
using System.Collections.Generic;
using System.Linq;

namespace firstTryASPnet.Repositories
{
    public class ReservationRepository
    {
        private readonly HotelContext _context;

        public ReservationRepository(HotelContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAll() => _context.Reservations.ToList();

        public Reservation? GetById(int id) => _context.Reservations.FirstOrDefault(r => r.Id == id);

        public void Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
    }
}
namespace firstTryASPnet.Repository
{
    public class ReservationRepository
    {
    }
}
