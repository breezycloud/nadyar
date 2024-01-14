using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Category
    {
        [Key] public Guid Id { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
