using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Branch
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Branch Name is required")]
        public string? BranchName { get; set; }
        public string? BranchAddress { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
