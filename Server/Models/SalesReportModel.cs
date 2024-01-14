using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SalesReportModel
    {
        public string? Date { get; set; }        
        public string? ItemName { get; set; }
        public string? Category { get; set; }
        public int SoldQty { get; set; }
        public string? Currency { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal TotalBuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal[]? SellPriceArray { get; set; }
        public decimal TotalSellPrice => SellPriceArray!.Sum();
        public decimal Profit => SellPrice - BuyPrice;
        public decimal GrossProfit { get; set; }
    }
}
