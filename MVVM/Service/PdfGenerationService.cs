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

    // Service class for generating PDF invoices for restaurant orders.

    public class PdfGenerationService
    {
        // Asynchronously generates a PDF file for a given order.
        // order: The order details to be included in the PDF.
        // filePath: The file path where the PDF will be saved.


        public async Task GenerateOrderPdfAsync(OrderModel order, string filePath)
        {

            // Running the PDF generation task on a separate thread to avoid blocking the main UI thread.

            await Task.Run(() =>
            {
                // Create a new PDF document.


                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);
                        // Set default font size to 14 for the document.

                        page.PageColor(QuestPDF.Helpers.Colors.White); // Fix: Use QuestPDF's Colors.White directly
                        page.DefaultTextStyle(x => x.FontSize(14));

                        // Add the company name in bold and center-aligned with a blue color.

                        page.Content()
                            .Column(col =>
                            {
                                col.Item().Text("Nickel City Brew Works")
                                    .Bold().FontSize(20).AlignCenter().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                                col.Item().PaddingVertical(5).Text($"Order ID: {order.Id}");
                                // Add the order date.

                                col.Item().Text($"Date: {order.OrderDate}");
                                col.Item().Text($"Payment Mode: {order.PaymentMode}");

                                // Add the total number of items in the order.

                                col.Item().Text($"Total Items: {order.TotalItemsCount}");
                                col.Item().Text($"Total Paid: ${order.TotalAmountPaid:F2}");

                                // Add a horizontal line for visual separation.

                                col.Item().PaddingVertical(15).LineHorizontal(1);

                                // Add a thank you message at the bottom.

                                col.Item().Text("Thank you for your order!").Italic();
                            });
                    });
                }).GeneratePdf(filePath);

                // Generate the PDF and save it to the provided file path.

            });
        }
    }
}
