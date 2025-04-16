using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Infrastructure;
using RestaurantPOS.Models;
using QuestPDF.Helpers;
using
 Microsoft.Maui.Storage;

namespace RestaurantPosMAUI.MVVM.Service
{

    public class PdfGenerationService
    {

        public async Task GenerateOrderPdfAsync(OrderModel order, string filePath)
        {
            await Task.Run(() =>
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);
                        page.PageColor(QuestPDF.Helpers.Colors.White); // Fix: Use QuestPDF's Colors.White directly
                        page.DefaultTextStyle(x => x.FontSize(14));

                        page.Content()
                            .Column(col =>
                            {
                                col.Item().Text("Nickel City Brew Works")
                                    .Bold().FontSize(20).AlignCenter().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                                col.Item().PaddingVertical(5).Text($"Order ID: {order.Id}");
                                col.Item().Text($"Date: {order.OrderDate}");
                                col.Item().Text($"Payment Mode: {order.PaymentMode}");
                                col.Item().Text($"Total Items: {order.TotalItemsCount}");
                                col.Item().Text($"Total Paid: ${order.TotalAmountPaid:F2}");
                                col.Item().PaddingVertical(15).LineHorizontal(1);
                                col.Item().Text("Thank you for your order!").Italic();
                            });
                    });
                }).GeneratePdf(filePath);
            });
        }
    }
}
