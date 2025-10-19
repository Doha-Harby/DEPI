using System;
using System.ComponentModel.DataAnnotations;

namespace firstTryASPnet.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Range(1, 999)]
        public int RoomNumber { get; set; }

        [StringLength(250)]
        public string? Notes { get; set; }
    }
}
