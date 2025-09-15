using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Task_NewsPortal.Models
{
    internal class NewsPortalContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-24COVKG;Database=NewsDb;Trusted_Connection=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Catalogs
            modelBuilder.Entity<Catalog>().HasData(
                new Catalog { Id = 1, Name = "Politics", Description = "Political news and updates" },
                new Catalog { Id = 2, Name = "Sports", Description = "Sports news and events" },
                new Catalog { Id = 3, Name = "Technology", Description = "Latest technology trends" }
            );

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "John Smith", Age = 35, Email = "john@example.com", Password = "1234", NationalId = "A123456", PhoneNumber = "01011111111" },
                new Author { Id = 2, Name = "Sara Ali", Age = 29, Email = "sara@example.com", Password = "5678", NationalId = "B987654", PhoneNumber = "01022222222" }
            );

            // Seed News
            modelBuilder.Entity<News>().HasData(
                new News { Id = 1, Title = "Election Results", Description = "The results of the national election...", Time = new TimeSpan(14, 30, 0), Date = new DateOnly(2025, 9, 15), AuthorId = 1, CatalogId = 1 },
                new News { Id = 2, Title = "Football Championship", Description = "Final match of the championship...", Time = new TimeSpan(20, 0, 0), Date = new DateOnly(2025, 9, 14), AuthorId = 2, CatalogId = 2 },
                new News { Id = 3, Title = "AI Breakthrough", Description = "New AI model surpasses human performance...", Time = new TimeSpan(10, 0, 0), Date = new DateOnly(2025, 9, 13), AuthorId = 1, CatalogId = 3 }
            );
        }
    }
    
}
