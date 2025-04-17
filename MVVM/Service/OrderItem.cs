using SQLite;

namespace RestaurantPOS.Data
{

    // Represents an item in an order, capturing the details of the menu item and its quantity.

    public class OrderItem
    {

        // Primary key for the OrderItem table, automatically increments with each new entry.
        [PrimaryKey, AutoIncrement]

        // The unique identifier for the associated order.

        public int Id { get; set; }

        // The unique identifier for the menu item in this order.

        public int OrderId { get; set; }

        // The name of the menu item.

        public int MenuItemId { get; set; }
        public string Name { get; set; }

        // The icon representing the menu item (possibly a path to an image).

        public string Icon { get; set; }

        // The price of the menu item.

        public decimal Price { get; set; }

        // The quantity of the menu item ordered.

        public int Quantity { get; set; }

        [Ignore]
        // Calculates and returns the total amount for this order item (Price * Quantity).

        public decimal Amount => Price * Quantity;
       
        //public object Item { get; internal set; }
    }
}
