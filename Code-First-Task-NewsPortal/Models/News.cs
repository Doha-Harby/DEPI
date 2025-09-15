using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Task_NewsPortal.Models
{
    internal class News
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Column(TypeName ="time")]
        public TimeSpan? Time { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? Date { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int CatalogId { get; set; }
        public Catalog? Catalog { get; set; }

        public override string ToString()
        {
            return
              $"Id: {Id}\n" +
              $"Title: {Title}\n" +
              $"Description: {Description}\n" +
              $"Catalog Id: {CatalogId}\n" +
              $"Date: {Date}, Time: {Time}\n" +
              "============================";
        }
    }
}
