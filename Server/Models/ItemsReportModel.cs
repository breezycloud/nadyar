using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ItemsReportModel
    {                
        public string? ItemName { get; set; }
        public string? Category { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int CurrentQty { get; set; }
        public int SoldQty { get; set; }
    }
}
