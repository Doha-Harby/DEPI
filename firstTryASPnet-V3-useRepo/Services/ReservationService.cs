using firstTryASPnet.Models;
using firstTryASPnet.Repositories;
using System.Collections.Generic;

namespace firstTryASPnet.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repo;

        public ReservationService(ReservationRepository repo)
        {
            _repo = repo;
        }

        public List<Reservation> GetAllReservations() => _repo.GetAll();

        public Reservation? GetReservationById(int id) => _repo.GetById(id);

        public void AddReservation(Reservation reservation) => _repo.Add(reservation);
    }
}
