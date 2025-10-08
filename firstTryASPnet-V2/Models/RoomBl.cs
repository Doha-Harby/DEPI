using Microsoft.AspNetCore.Mvc;

namespace firstTryASPnet.Models
{
    public class RoomBl 
    {
        List<Room> room;
       public RoomBl()
        {
            room = new List<Room>();
            room.Add(new Room() { RoomNumber = 106, RoomType = "Suite", Price = 1600, Availability = "Occupied", Notes = "VIP room" });
            room.Add(new Room() { RoomNumber = 105, RoomType = "Double", Price = 850, Availability = "Available", Notes = "Balcony" });
            room.Add(new Room() { RoomNumber = 101, RoomType = "Single", Price = 500, Availability = "Available", Notes = "Sea view" });
            room.Add(new Room() { RoomNumber = 102, RoomType = "Double", Price = 800, Availability = "Occupied", Notes = "Near elevator" });
            room.Add(new Room() { RoomNumber = 103, RoomType = "Suite", Price = 1500, Availability = "Available", Notes = "Corner room" });
            room.Add(new Room() { RoomNumber = 104, RoomType = "Single", Price = 550, Availability = "Maintenance", Notes = "Under renovation" });
        }

        public List<Room> GetAll()
        {
            return room;
        }

        public Room GetByRoomNumber(int roomNumber)
        {
            return room.FirstOrDefault(e => e.RoomNumber == roomNumber);
        }
    }
}
