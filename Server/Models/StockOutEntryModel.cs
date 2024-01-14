using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class StockOutEntryModel
{
    public Guid ItemID { get; set; }
    public Stock? Stock { get; set; }
    public long ModifiedTicks { get; set; }
}
