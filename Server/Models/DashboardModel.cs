using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class DashboardModel
{
    public int TotalBranches { get; set; }
    public int Categories { get; set; }
    public int TotalCustomers { get; set; }
    public int AvailableProducts { get; set; }
    public int SoldProducts { get; set; }
    public int TotalEmployees { get; set; }
    public ItemSalesLine[] ItemSales { get; set; } = Array.Empty<ItemSalesLine>();        
    public Dictionary<string, int>? ServiceTopCustomer { get; set; }
    public Dictionary<string, int>? ProductTopCustomer { get; set; }
    public bool IsBusy { get; set; }
}

public class ItemSalesLine
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int? Sales { get; set; }
}
