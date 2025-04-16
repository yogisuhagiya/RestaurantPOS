using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPOS.Data;
using System.Windows.Input;

namespace RestaurantPOS.Models
{
    public partial class OrderModel : ObservableObject
    {
        //  This will be store the data in user Order items.
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalItemsCount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public string PaymentMode { get; set; } // Cash or Online

        public OrderItem[] Items { get; set; }

        //public OrderItem[] Items
        //{
        //    get;
        //    set;
        //} = Array.Empty<OrderItem>();
        [ObservableProperty]
        private bool _isSelected;

        [ObservableProperty]
        private ICommand _printBillCommand;

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
