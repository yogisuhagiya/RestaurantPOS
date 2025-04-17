using SQLite;

namespace RestaurantPOS.Data
{

    // Represents an order in the SQLite database.

    public class Order 
    {

        // Primary key for the Order table, automatically increments with each new entry.

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The date and time when the order was placed.

        public DateTime OrderDate { get; set; }

        // The total number of items included in the order.

        public int TotalItemsCount { get; set; }

        // The total amount paid for the order.

        public decimal TotalAmountPaid { get; set; }

        // The payment method used for the order (e.g., Cash or Online).

        public string PaymentMode { get; set; } // Cash or Online
 
    }
}
