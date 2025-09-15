using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First_Task_NewsPortal.Models
{
    internal class Catalog
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        
        public string? Description { get; set; }

        public ICollection<News> News { get; } = new List<News>();
    }
}
