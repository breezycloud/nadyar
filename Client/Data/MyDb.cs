using BlazorDexie.Database;
using BlazorDexie.JsModule;
using Shared.Models;

namespace Client.Data;
public class MyDb : Db
{
    public Store<Branch, Guid> Branches { get; set; } = new("Id", "BranchName");
    public Store<Category, Guid> Categories { get; set; } = new(nameof(Category.Id), nameof(Category.CategoryName), nameof(Category.ModifiedDate));
    public Store<Item, Guid> Items { get; set; } = new(nameof(Item.Id), "ItemName", "BranchID", nameof(Item.Branch),  nameof(Item.Quantity), nameof(Item.Category), nameof(Item.StocksIn), nameof(Item.StocksOut), nameof(Item.ModifiedTicks));        
    public MyDb(IModuleFactory moduleFactory)
        : base("MyDb", 1, new DbVersion[] { }, moduleFactory)
    {
    }
}
