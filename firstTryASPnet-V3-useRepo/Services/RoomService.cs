using firstTryASPnet.Models;
using firstTryASPnet.Repositories;
using System.Collections.Generic;

namespace firstTryASPnet.Services
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }

        public Room GetRoomById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public void AddRoom(Room room)
        {
            if (room.Price <= 0)
                throw new Exception("Room price must be greater than zero.");

            _roomRepository.Add(room);
        }
    }
}
