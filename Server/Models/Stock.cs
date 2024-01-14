using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

public class Stock
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Date field is required")]
    public DateOnly Date { get; set; }
    [Required(ErrorMessage = "Quantity is required")]
    public int? Quantity { get; set; } = 0;    
    public bool IsDeleted { get; set; } = false;
    public long ModifiedTicks { get; set; } = DateTime.Now.Ticks;
}
