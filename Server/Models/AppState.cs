using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class AppState
    {
        public event EventHandler? OnUpdate;
        public void UpdateLayout() => OnUpdate?.Invoke(this, EventArgs.Empty);
        public Category Category { get; set; } = default!;
        public Category[] Categories { get; set; } = [];
        public List<StockOutEntryModel> Items { get; set; } = new();
        public Item Item { get; set; } = default!;
        public List<Item> Products { get; set; } = new();
        public List<Item> StockOut { get; set; } = new();
        public bool IsRows { get; set; } = false;
        public string? searchString = "";
        public bool IsProcessing { get; set; }
        public bool IsBusy { get; set; }
        public bool IsConnected { get; set; }
        public bool IsUpload { get; set; }
        public bool IsDownload { get; set; }
        public string? Token { get; set; }        

        public Exception? syncException { get; set; }
        public void HandleSyncError(Exception ex)
        {
            syncException = ex;
        }

        public DateOnly ParseStringDate(string? text)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var d = DateTime.ParseExact(text!, "dd/MM/yyyy", provider);        
            return DateOnly.FromDateTime(d);
        }
    }
}
