using Microsoft.EntityFrameworkCore;
using firstTryASPnet.Models;

namespace firstTryASPnet.Models
{
    public class HotelContext : DbContext
    {
        public DbSet<Room> Room { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public HotelContext() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-24COVKG;Database=Hotel;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
           
        }
    }
}
