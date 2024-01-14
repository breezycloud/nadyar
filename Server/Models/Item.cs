using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Item
{
    [Key] public Guid Id { get; set; }
    public Guid BranchID { get; set; }
    public virtual Branch? Branch { get; set; }
    public virtual Category? Category { get; set; }
    [Required(ErrorMessage = "Name is required!")]
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    [Required(ErrorMessage = "Piece is required!")]
    public int? Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [Required(ErrorMessage = "Amount is required!")]
    public decimal? BuyPrice { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    [Required(ErrorMessage = "Amount is required!")]
    public decimal? SellPrice { get; set; }        
    public bool IsDeleted { get; set; } = false;
    public DateTime ModifiedTicks { get; set; } = DateTime.Now;       
    public virtual List<Stock>? StocksIn { get; set; } = new();
    public virtual List<Stock>? StocksOut { get; set; } = new();
}
