using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPOS.Data;
using System.Windows.Input;

namespace RestaurantPOS.Models
{
    // Represents a customer's order in the restaurant POS system
    public partial class OrderModel : ObservableObject
    {
        // Unique identifier for the order
        public int Id { get; set; }

        // Date and time when the order was placed
        public DateTime OrderDate { get; set; }

        // Total number of items in the order
        public int TotalItemsCount { get; set; }

        // Total amount paid for the order
        public decimal TotalAmountPaid { get; set; }

        // Payment method used for the order (e.g., Cash, Online)
        public string PaymentMode { get; set; } // Cash or Online

        // Array of individual items in the order
        public OrderItem[] Items { get; set; }

        //public OrderItem[] Items
        //{
        //    get;
        //    set;
        //} = Array.Empty<OrderItem>();

        // Indicates whether the order is currently selected in the UI

        [ObservableProperty]
        private bool _isSelected;

        // Command to trigger bill printing functionality
        [ObservableProperty]
        private ICommand _printBillCommand;

        // Prepare the bill content as a formatted string
        public void PrintBill()
        {
            // Check if the Items array is null or empty
            if (Items == null || Items.Length == 0)
            {
                Console.WriteLine("No items in the order to print.");
                return;
            }

            // Logic to print the bill
            var billContent = $"Order Id: {Id}\n" +
                              $"Order Date: {OrderDate:dd/MM/yyyy HH:mm:ss}\n" +
                              $"Total Amount: {TotalAmountPaid:C}\n" +
                              $"Payment Mode: {PaymentMode}\n" +
                              "Items:\n";

            // Append details of each item in the order

            foreach (var item in Items)
            {
                // Check if the current item is null
                if (item == null)
                {
                    continue; // Skip null items
                }

                billContent += $"{item.Name} x {item.Quantity} - {item.Price:C} = {item.Amount:C}\n";
            }

            // Here, implement the print functionality, for example, using a printing library or generating a PDF
            Console.WriteLine(billContent);  // Placeholder for actual print logic
        }




    }
}
