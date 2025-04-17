using CommunityToolkit.Mvvm.ComponentModel;

namespace RestaurantPOS.Models
{
    // Represents a single item in the cart

    public partial class CartModel : ObservableObject
    {

        // Unique identifier of the menu item

        public int ItemId { get; set; }

        // Name of the item
        public string Name { get; set; }

        // Path or resource name for the item's icon image
        public string Icon { get; set; }

        // Price per unit of the item
        public decimal Price { get; set; }

        // Quantity of the item added to the cart (default is 1000 for testing or demo purposes)

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]

        private int _quantity = 1000;

        // Total price calculated based on quantity and price

        public decimal Amount => Price * Quantity;
    }
}
