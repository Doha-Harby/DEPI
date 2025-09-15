using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Task_NewsPortal.Models
{
    internal class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Password { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Email { get; set; }

        [Required]
        public string? NationalId { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<News> NewsList { get; } = new List<News>();
    }
}
