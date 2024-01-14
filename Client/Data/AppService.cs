using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;
using Microsoft.JSInterop;
using Shared;
using BlazorDexie.JsModule;
using Bogus;

namespace Client.Data.AppService;

public interface IAppService
{
    ValueTask<DashboardModel> GetDashboard();
    Task SeedData();
}
public class AppService : IAppService
{
    private readonly IJSRuntime _js;
    private EsModuleFactory moduleFactory;
    public AppService(IJSRuntime js)
    {
        _js = js;
        moduleFactory = new EsModuleFactory(js);

    }

    public async Task SeedData()
    {
        var branches = new List<Branch>()
        {  
            new Branch()
            {
                Id = Guid.NewGuid(),
                BranchName = "Kasuwa",
                BranchAddress = "Central Market",
            },
            new Branch()
            {
                Id = Guid.NewGuid(),
                BranchName = "GRA",
                BranchAddress = "GRA",
            }
        };
        var db = new MyDb(moduleFactory);
        var categories = new Faker().Commerce.Categories(100);
        var faker1 = new Faker<Category>().RuleFor(x => x.CategoryName, f => f.PickRandom(categories));
        Console.WriteLine("Generating categories...");
        var fakerCategories = faker1.Generate(100);
        fakerCategories.ForEach(x => x.Id = Guid.NewGuid());

        var faker2 = new Faker<Item>().RuleFor(x => x.ItemName, f => f.Commerce.ProductName())
                                      .RuleFor(x => x.Branch,f => f.PickRandom(branches))
                                      .RuleFor(x => x.Category, f => f.PickRandom(fakerCategories))
                                      .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                                      .RuleFor(x => x.Quantity, new Random().Next(1, 10))
                                      .RuleFor(x => x.BuyPrice, f => decimal.Parse(f.Commerce.Price(min: 500, decimals: 2, symbol: "")))
                                      .RuleFor(x => x.SellPrice, f => decimal.Parse(f.Commerce.Price(min: 1500, decimals: 2, symbol: "")))
                                      .RuleFor(x => x.ModifiedTicks, f => f.Date.Recent());
        Console.WriteLine("Generating items...");
        var fakerProducts = faker2.Generate(1000);
        fakerProducts.ForEach((item) =>
        {
            item.Id = Guid.NewGuid();
            item.BranchID = item.Branch!.Id;
            item.StocksIn = new List<Stock>
            {
                new Stock
                {
                    Id = Guid.NewGuid(),
                    Date =  DateOnly.FromDateTime(DateTime.Now),
                    Quantity = item.Quantity,
                }
            };
        });

        Console.WriteLine("Adding branches...");
        await db.Branches.BulkAdd(branches);
        Console.WriteLine("Adding categories...");
        await db.Categories.BulkAdd(fakerCategories);
        Console.WriteLine("Adding items...");
        await db.Items.BulkAdd(fakerProducts);
        Console.WriteLine("Data Saved");
    }

    public async ValueTask<DashboardModel> GetDashboard()
    {
        DashboardModel model = new();
        var db = new MyDb(moduleFactory);
        var SoldProducts = await db.Items.ToArray();
        model.AvailableProducts = await db.Items.Where(nameof(Item.Quantity)).AboveOrEqual(1).Count();                
        model.SoldProducts = SoldProducts.SelectMany(x=> x.StocksOut!).Sum(x => x.Quantity)!.Value;
        model.Categories = await db.Categories.Count();        
        model.TotalBranches = await db.Branches.Count();

        //model.Items = SoldProducts.GroupBy(x => x.Id).Select(y => new ItemChartModel
        //                                {
        //                                    ServiceName = y.Select(i => i.Item!.ItemName)
        //                                                    .FirstOrDefault(),
        //                                    SalesCount = y.Select(i => i.ItemId).Count()
        //                                }).OrderByDescending(x => x.SalesCount).Take(10).ToArray();
        model.ItemSales = SoldProducts.SelectMany(x => x.StocksOut!).GroupBy(x => new { x.Date.Year, x.Date.Month }).Select(y => new ItemSalesLine
        {
            Year = y.Key.Year,
            Month = y.Key.Month,
            Sales = y.Count(x => x.Date.Month == y.Key.Month && x.Date.Year == y.Key.Year)
        }).ToArray();

        return model;
    }
}
