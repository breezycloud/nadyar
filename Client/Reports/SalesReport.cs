using Blazored.LocalStorage;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Linq;
using Shared.Models;
using Microsoft.AspNetCore.Components.Routing;

namespace Client.Reports;

public class SalesReport
{
    public SalesReport(List<SalesReportModel>? model)
    {
        Model = model;
    }
    private List<SalesReportModel>? Model { get; set; }

    public byte[] Create()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Portrait());
                page.MarginTop(0, Unit.Inch);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

            });
        });
        return document.GeneratePdf();
    }
    void ComposeHeader(IContainer container)
    {
        container.AlignLeft().Column(column =>
        {
            column.Spacing(5);
            //column.Item().AlignCenter().Width(30, Unit.Millimetre).Height(30, Unit.Millimetre).Image(Logo, ImageScaling.FitArea);
            column.Item().AlignLeft().Text("Nadyar").Bold().FontSize(20);
            column.Item().AlignLeft().Text("Sales Report").FontSize(15);
            column.Item().AlignLeft().Text($"Date {Model!.Select(x => x.Date).First()}").FontSize(10);
        });
    }
    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Padding(1.5f).Column(column =>
        {            
            column.Spacing(10);
            column.Item().Element(ComposeTable);
        });
    }

    void ComposeTable(IContainer container)
    {

        container.PaddingVertical(1.2f).Padding(1.2f).Border(1f).BorderColor(Colors.Grey.Medium).Table(table =>
        {
            table.ColumnsDefinition(column =>
            {
                column.RelativeColumn(0.5f);
                column.RelativeColumn(2.5f);
                column.RelativeColumn(2.5f);
                column.RelativeColumn(1.5f);
                column.RelativeColumn(1.5f);
                column.RelativeColumn(1.5f);
                column.RelativeColumn(1.5f);
                column.RelativeColumn(1.5f);
            });

            table.Header(header =>
            {
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("S/No").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Item").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Category").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Buy Price").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Sell Price").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("Sold Quantity").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Total Sell Price").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Profit").FontSize(10);
                // header.Cell().ColumnSpan(5)
                //     .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
            });


            // step 3
            foreach (var item in Model!)
            {
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text(Model.IndexOf(item) + 1).FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text(item.ItemName).FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text($"{item.Category}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.BuyPrice:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.SellPrice:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text($"{item.SoldQty}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.TotalSellPrice:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.Profit:N2}").FontSize(10);
            }
            table.Cell();
            table.Cell();
            table.Cell();
            table.Cell();
            table.Cell();
            table.Cell();
            table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.TotalSellPrice):N2}");
            table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.Profit):N2}");

            //table.Footer(footer =>
            //{
            //    footer.Cell().RowSpan(8).ColumnSpan(8).Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.TotalSellPrice):N2}");
            //    footer.Cell().RowSpan(8).ColumnSpan(8).Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.Profit):N2}");
            //});
        });
    }
}
