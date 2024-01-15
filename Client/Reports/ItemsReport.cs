using Blazored.LocalStorage;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Linq;
using Shared.Models;

namespace Client.Reports;

public class ItemsReport
{
    private string Branch { get; set; }
    public ItemsReport(List<ItemsReportModel>? model, string? reportHeader, string branch)
    {
        Model = model;
        ReportHeader = reportHeader;
        Branch = branch;
    }
    public string? ReportHeader { get; set; }
    private List<ItemsReportModel>? Model { get; set; }

    public byte[] Create()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Portrait());
                page.MarginTop(0, Unit.Inch);

                page.Header().ShowOnce().Element(ComposeHeader);
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
            column.Item().AlignLeft().Text("NADYAR STORE").Bold().FontSize(20);            
            column.Item().AlignLeft().Text(Branch).Bold().FontSize(15);
            column.Item().AlignLeft().Text("Stock Report").FontSize(13);
            column.Item().AlignLeft().Text(ReportHeader).FontSize(10);
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
            });

            table.Header(header =>
            {
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("S/NO").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Item").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Category").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("Current Quantity").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("Sold Quantity").FontSize(10);
                // header.Cell().ColumnSpan(5)
                //     .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
            });

            
            // step 3
            foreach (var item in Model!)
            {
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text(Model.IndexOf(item) + 1).FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text(item.ItemName).FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text($"{item.Category}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text($"{item.CurrentQty}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text($"{item.SoldQty}").FontSize(10);                
            }
        });
    }
}
