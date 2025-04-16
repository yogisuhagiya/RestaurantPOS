using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPOS.Data;
using RestaurantPOS.Models;
using System.Collections.ObjectModel;
using RestaurantPosMAUI.MVVM.Service;
using RestaurantPOS;
using System.Windows.Input;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.Maui.Storage;
using QuestPDF.Drawing;
using Microsoft.Maui.ApplicationModel;
using RestaurantPosMAUI.MVVM;
using CommunityToolkit.Maui.Core;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

using Microsoft.Maui.Storage;
using Raven.Database.Util;




namespace RestaurantPOS.ViewModels
{
    public partial class OrdersViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<OrderModel> Orders { get; set; } = new ObservableCollection<OrderModel>();
        private

        readonly
         PdfGenerationService _pdfGenerationService;

        public OrdersViewModel(DatabaseService databaseService, PdfGenerationService pdfGenerationService)
        {
            _databaseService = databaseService;
            _pdfGenerationService = pdfGenerationService;

        }

        [RelayCommand]
        //public async Task PrintBillAsync(OrderModel order)
        //{
        //    try
        //    {
        //        // Ensure the order is available before proceeding
        //        if (order == null)
        //        {
        //            await Toast.Make("Error: Order not found", ToastDuration.Long).Show();
        //            Console.WriteLine("Order not found.");
        //            return;
        //        }

        //        // Generate the file path for the PDF file
        //        string fileName = $"Bill_Order_{order.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        //        string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        //        // Create a PDF document for the order
        //        using (var doc = new PdfDocument())
        //        {
        //            var page = doc.AddPage();
        //            var graphics = XGraphics.FromPdfPage(page);
        //            var fontRegular = new XFont("Verdana", 12, XFontStyle.Regular);
        //            var fontBold = new XFont("Verdana", 14, XFontStyle.Bold);
        //            var fontHeader = new XFont("Verdana", 16, XFontStyle.Bold);

        //            // Starting Y position
        //            int yPosition = 20;

        //            // Header Section
        //            graphics.DrawString("🍽️ My Restaurant", fontHeader, XBrushes.Black, 150, yPosition);
        //            yPosition += 40;

        //            graphics.DrawString("Order Bill", fontBold, XBrushes.Black, 220, yPosition);
        //            yPosition += 40;

        //            // Order Details Section
        //            graphics.DrawString($"Order ID: {order.Id}", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 25;
        //            graphics.DrawString($"Date: {order.OrderDate:yyyy-MM-dd}", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 25;
        //            graphics.DrawString($"Payment Mode: {order.PaymentMode}", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 40;

        //            // Line Separator (after order details)
        //            graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
        //            yPosition += 20;

        //            // Table Header: Itemized list
        //            graphics.DrawString("Item                           Qty    Price     Total", fontBold, XBrushes.Black, 20, yPosition);
        //            yPosition += 25;

        //            // Line separator after header
        //            graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
        //            yPosition += 20;

        //            // Order Item Listings (static data for demonstration)
        //            graphics.DrawString("Burger                         2      $5.00    $10.00", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 30;

        //            graphics.DrawString("Pizza                          1      $8.00    $8.00", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 30;

        //            graphics.DrawString("Soda                           3      $1.50    $4.50", fontRegular, XBrushes.Black, 20, yPosition);
        //            yPosition += 30;

        //            // Line Separator (after items)
        //            graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
        //            yPosition += 20;

        //            // Total Section
        //            graphics.DrawString($"Total Items: {order.TotalItemsCount}", fontBold, XBrushes.Black, 20, yPosition);
        //            yPosition += 25;
        //            graphics.DrawString($"Total Paid: ${order.TotalAmountPaid:F2}", fontBold, XBrushes.Black, 20, yPosition);
        //            yPosition += 40;

        //            // Line Separator (before footer)
        //            graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
        //            yPosition += 20;

        //            // Footer Section
        //            graphics.DrawString("Thank you for your order!", fontRegular, XBrushes.Black, 200, yPosition);

        //            // Save the PDF to the specified path
        //            doc.Save(filePath);
        //        }

        //        // Check if the PDF file has been generated successfully
        //        if (File.Exists(filePath))
        //        {
        //            // Attempt to open the generated PDF file
        //            await Launcher.Default.OpenAsync(new OpenFileRequest
        //            {
        //                File = new ReadOnlyFile(filePath)
        //            });

        //            // Show success message
        //            await Toast.Make("PDF successfully generated and opened!", ToastDuration.Long).Show();
        //            Console.WriteLine("Order PDF generated at: " + filePath);
        //        }
        //        else
        //        {
        //            // Show error message if the PDF generation failed
        //            await Toast.Make("Error: PDF generation failed for order.", ToastDuration.Long).Show();
        //            Console.WriteLine("Order PDF generation failed.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Capture and display the error message
        //        string errorMessage = $"Exception: {ex.Message}";
        //        string stackTrace = ex.StackTrace;

        //        // If there's an inner exception, capture that too
        //        if (ex.InnerException != null)
        //        {
        //            errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
        //            stackTrace += $"\nInner Stack Trace: {ex.InnerException.StackTrace}";
        //        }

        //        // Display the error message and stack trace for debugging
        //        await Toast.Make($"Error: {errorMessage}", ToastDuration.Long).Show();
        //        Console.WriteLine("Exception details: ");
        //        Console.WriteLine("Message: " + errorMessage);
        //        Console.WriteLine("Stack Trace: " + stackTrace);
        //    }
        //}


        public async Task PrintBillAsync(OrderModel order)
        {
            try
            {
                // Ensure the order is available before proceeding
                if (order == null)
                {
                    await Toast.Make("Error: Order not found", ToastDuration.Long).Show();
                    Console.WriteLine("Order not found.");
                    return;
                }

                // Generate the file path for the PDF file
                string fileName = $"Bill_Order_{order.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Create a PDF document for the order
                using (var doc = new PdfDocument())
                {
                    var page = doc.AddPage();
                    var graphics = XGraphics.FromPdfPage(page);
                    var fontRegular = new XFont("Verdana", 12, XFontStyle.Regular);
                    var fontBold = new XFont("Verdana", 14, XFontStyle.Bold);
                    var fontHeader = new XFont("Verdana", 16, XFontStyle.Bold);

                    // Starting Y position
                    int yPosition = 20;

                    // Header Section
                    graphics.DrawString("🍽️ My Restaurant", fontHeader, XBrushes.Black, 150, yPosition);
                    yPosition += 40;

                    graphics.DrawString("Order Bill", fontBold, XBrushes.Black, 220, yPosition);
                    yPosition += 40;

                    // Order Details Section
                    graphics.DrawString($"Order ID: {order.Id}", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 25;
                    graphics.DrawString($"Date: {order.OrderDate:yyyy-MM-dd}", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 25;
                    graphics.DrawString($"Payment Mode: {order.PaymentMode}", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 40;

                    // Line Separator (after order details)
                    graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                    yPosition += 20;

                    // Table Header: Itemized list (Adjusted for alignment)
                    graphics.DrawString("Item                          Qty  Price  Total", fontBold, XBrushes.Black, 20, yPosition);
                    yPosition += 25;

                    // Line separator after header
                    graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                    yPosition += 20;

                    // Order Item Listings (with fixed width for alignment)
                    graphics.DrawString("Burger                        2    $5.00  $10.00", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 25;

                    graphics.DrawString("Pizza                         1    $8.00  $8.00", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 25;

                    graphics.DrawString("Soda                          3    $1.50  $4.50", fontRegular, XBrushes.Black, 20, yPosition);
                    yPosition += 25;

                    // Line Separator (after items)
                    graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                    yPosition += 20;

                    // Total Section
                    graphics.DrawString($"Total Items: {order.TotalItemsCount}", fontBold, XBrushes.Black, 20, yPosition);
                    yPosition += 25;
                    graphics.DrawString($"Total Paid: ${order.TotalAmountPaid:F2}", fontBold, XBrushes.Black, 20, yPosition);
                    yPosition += 40;

                    // Line Separator (before footer)
                    graphics.DrawLine(XPens.Black, 20, yPosition, page.Width - 20, yPosition);
                    yPosition += 20;

                    // Footer Section
                    graphics.DrawString("Thank you for your order!", fontRegular, XBrushes.Black, 200, yPosition);

                    // Save the PDF to the specified path
                    doc.Save(filePath);
                }

                // Check if the PDF file has been generated successfully
                if (File.Exists(filePath))
                {
                    // Attempt to open the generated PDF file
                    await Launcher.Default.OpenAsync(new OpenFileRequest
                    {
                        File = new ReadOnlyFile(filePath)
                    });

                    // Show success message
                    await Toast.Make("PDF successfully generated and opened!", ToastDuration.Long).Show();
                    Console.WriteLine("Order PDF generated at: " + filePath);
                }
                else
                {
                    // Show error message if the PDF generation failed
                    await Toast.Make("Error: PDF generation failed for order.", ToastDuration.Long).Show();
                    Console.WriteLine("Order PDF generation failed.");
                }
            }
            catch (Exception ex)
            {
                // Capture and display the error message
                string errorMessage = $"Exception: {ex.Message}";
                string stackTrace = ex.StackTrace;

                // If there's an inner exception, capture that too
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                    stackTrace += $"\nInner Stack Trace: {ex.InnerException.StackTrace}";
                }

                // Display the error message and stack trace for debugging
                await Toast.Make($"Error: {errorMessage}", ToastDuration.Long).Show();
                Console.WriteLine("Exception details: ");
                Console.WriteLine("Message: " + errorMessage);
                Console.WriteLine("Stack Trace: " + stackTrace);
            }
        }









        //public async Task PrintBillAsync(OrderModel order)
        //{
        //    try
        //    {
        //        // Ensure the order is available before proceeding
        //        if (order == null)
        //        {
        //            await Toast.Make("Error: Order not found", ToastDuration.Long).Show();
        //            Console.WriteLine("Order not found.");
        //            return;
        //        }

        //        // Generate the file path for the PDF file
        //        string fileName = $"Bill_Order_{order.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        //        string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        //        // Create a PDF document for the order
        //        using (var doc = new PdfDocument())
        //        {
        //            var page = doc.AddPage();
        //            var graphics = XGraphics.FromPdfPage(page);
        //            var font = new XFont("Verdana", 14, XFontStyle.Regular);

        //            // Start writing the order details into the PDF
        //            graphics.DrawString("Welcome to My Restaurant !!", font, XBrushes.Black, 20, 20);
        //            graphics.DrawString("Hello, Yogi", font, XBrushes.Black, 20, 90);
        //            graphics.DrawString($"Order ID: {order.Id}", font, XBrushes.Black, 20, 120);
        //            graphics.DrawString($"Date: {order.OrderDate.ToString("yyyy-MM-dd")}", font, XBrushes.Black, 20, 140);
        //            graphics.DrawString($"Payment Mode: {order.PaymentMode}", font, XBrushes.Black, 20, 160);
        //            graphics.DrawString($"Total Items: {order.TotalItemsCount}", font, XBrushes.Black, 20, 180);
        //            graphics.DrawString($"Total Paid: ${order.TotalAmountPaid:F2}", font, XBrushes.Black, 20, 200);


        //            // Add a line break and a thank-you message
        //            graphics.DrawString("Thank you for your order!", font, XBrushes.Black, 20, 240);

        //            // Save the PDF to the specified path
        //            doc.Save(filePath);
        //        }

        //        // Check if the PDF file has been generated successfully
        //        if (File.Exists(filePath))
        //        {
        //            // Attempt to open the generated PDF file
        //            await Launcher.Default.OpenAsync(new OpenFileRequest
        //            {
        //                File = new ReadOnlyFile(filePath)
        //            });

        //            // Show success message
        //            await Toast.Make("PDF successfully generated and opened!", ToastDuration.Long).Show();
        //            Console.WriteLine("Order PDF generated at: " + filePath);
        //        }
        //        else
        //        {
        //            // Show error message if the PDF generation failed
        //            await Toast.Make("Error: PDF generation failed for order.", ToastDuration.Long).Show();
        //            Console.WriteLine("Order PDF generation failed.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Capture and display the error message
        //        string errorMessage = $"Exception: {ex.Message}";
        //        string stackTrace = ex.StackTrace;

        //        // If there's an inner exception, capture that too
        //        if (ex.InnerException != null)
        //        {
        //            errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
        //            stackTrace += $"\nInner Stack Trace: {ex.InnerException.StackTrace}";
        //        }

        //        // Display the error message and stack trace for debugging
        //        await Toast.Make($"Error: {errorMessage}", ToastDuration.Long).Show();
        //        Console.WriteLine("Exception details: ");
        //        Console.WriteLine("Message: " + errorMessage);
        //        Console.WriteLine("Stack Trace: " + stackTrace);
        //    }
        //}




        //[RelayCommand]
        //public async Task PrintBillAsync(OrderModel order)
        //{
        //    string filePath = string.Empty;  // Declare filePath outside the try block

        //    try
        //    {
        //        if (order != null)
        //        {
        //            // Generate a unique file name for each order
        //            string fileName = $"Bill_Order_{order.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

        //            // Define the file path where the PDF will be saved
        //            filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        //            // Log the file path for debugging
        //            Console.WriteLine("Saving PDF to: " + filePath);

        //            // Generate the PDF content
        //            Document.Create(container =>
        //            {
        //                container.Page(page =>
        //                {
        //                    page.Margin(50);
        //                    page.Size(PageSizes.A4);
        //                    page.PageColor(QuestPDF.Helpers.Colors.White);
        //                    page.DefaultTextStyle(x => x.FontSize(14));

        //                    page.Content()
        //                        .Column(col =>
        //                        {
        //                            col.Item().Text("Nickel City Brew Works").Bold().FontSize(20).AlignCenter().FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
        //                            col.Item().Text($"Order ID: {order.Id}");
        //                            col.Item().Text($"Date: {order.OrderDate}");
        //                            col.Item().Text($"Payment Mode: {order.PaymentMode}");
        //                            col.Item().Text($"Total Items: {order.TotalItemsCount}");
        //                            col.Item().Text($"Total Paid: ${order.TotalAmountPaid:F2}");
        //                            col.Item().PaddingVertical(15).LineHorizontal(1);
        //                            col.Item().Text("Thank you for your order!").Italic();
        //                        });
        //                });
        //            }).GeneratePdf(filePath);

        //            // Check if the PDF is created
        //            if (File.Exists(filePath))
        //            {
        //                // Show a success message
        //                await Toast.Make("Bill Printed Successfully", ToastDuration.Long).Show();

        //                // Open the PDF after generating
        //                await Launcher.Default.OpenAsync(new OpenFileRequest
        //                {
        //                    File = new ReadOnlyFile(filePath)
        //                });
        //            }
        //            else
        //            {
        //                // Show error if PDF wasn't generated
        //                await Toast.Make("Error: PDF generation failed.", ToastDuration.Long).Show();
        //            }
        //        }
        //        else
        //        {
        //            // If the order is null, show an error message
        //            await Toast.Make("Error: Order not found", ToastDuration.Long).Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = ex.Message;

        //        // Check if the file path exists after generating the PDF
        //        if (File.Exists(filePath))
        //        {
        //            Console.WriteLine("PDF file generated at: " + filePath);
        //            await Toast.Make("PDF successfully generated. Attempting to open...", ToastDuration.Long).Show();
        //        }
        //        else
        //        {
        //            Console.WriteLine("PDF generation failed.");
        //            await Toast.Make("Error: PDF generation failed.", ToastDuration.Long).Show();
        //        }

        //        // Show exception message in toast and log it
        //        await Toast.Make(errorMessage, ToastDuration.Long).Show();
        //        Console.WriteLine("Exception Message: " + errorMessage);
        //    }
        //}






        //[RelayCommand]
        //public async Task PrintBillAsync(OrderModel order)
        //{
        //    if (order != null)
        //    {
        //        try
        //        {
        //            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bill.pdf");

        //            // Get the bill content (customize as needed)
        //            string billContent = $"Order ID: {order.Id}\nTotal: {order.TotalAmountPaid}\nItems: {order.TotalItemsCount}";

        //            // Generate the PDF

        //            await Toast.Make("Bill Printed Successfully").Show();
        //        }
        //        catch (Exception ex)
        //        {
        //            await Toast.Make($"Error: {ex.Message}").Show();
        //        }
        //    }
        //    else
        //    {
        //        await Toast.Make("Error: Order not found").Show();
        //    }
        //}




        public async Task<bool> CreateOderAsync(CartModel[] cartItems, bool isPaidCash)
        {
            var orderItems = cartItems
                .Select(item => new OrderItem
                {
                    MenuItemId = item.ItemId,
                    Name = item.Name,
                    Icon = item.Icon,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToArray();

            var order = new OrderModel
            {
                OrderDate = DateTime.Now,
                PaymentMode = isPaidCash ? "Cash" : "Online",
                TotalAmountPaid = cartItems.Sum(i => i.Amount),
                TotalItemsCount = cartItems.Length,
                Items = orderItems
            };

            var errorMessage = await _databaseService.PlaceOrderAsync(order);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Shell.Current.DisplayAlert("Error", errorMessage, "OK");
                return false;
            }
            await Toast.Make("Order created successfully").Show();
            Orders.Add(order);
            return true;
        }

        private bool _isInitialized;

        [ObservableProperty]
        private bool _isLoading;

        public async ValueTask InitializeAsync()
        {
            if (_isInitialized) return;

            _isInitialized = true;

            IsLoading = true;

            Orders.Clear();

            var dbOrders = await _databaseService.GetOrdersAsync();

            var orders = dbOrders.Select(o => new OrderModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                PaymentMode = o.PaymentMode,
                TotalAmountPaid = o.TotalAmountPaid,
                TotalItemsCount = o.TotalItemsCount
            });

            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            IsLoading = false;
        }

        [ObservableProperty]
        private OrderItem[] _orderItems = [];

        [RelayCommand]
        public async Task SelectOrderAsync(OrderModel? order)
        {
            var prevSelectedOrder = Orders.FirstOrDefault(o => o.IsSelected);
            if (prevSelectedOrder != null)
            {
                prevSelectedOrder.IsSelected = false;
                if (prevSelectedOrder.Id == order?.Id)
                {
                    OrderItems = [];
                    return;
                }
            }

            if (order == null || order.Id == 0)
            {
                OrderItems = [];
                return;
            }

            IsLoading = true;
            order.IsSelected = true;
            OrderItems = await _databaseService.GetOrderItemsByOrderIdAsync(order.Id);
            IsLoading = false;
        }
    }
}
