using System.ComponentModel.DataAnnotations;

namespace firstTryASPnet.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public string Availability { get; set; }
        public string? Notes { get; set; }

        //public List<Room>? rooms { get; set; }
    }
}
