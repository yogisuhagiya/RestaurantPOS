using SQLite;

namespace RestaurantPOS.Data
{
    public class OrderItem
    {

        // This will be get and set the order details.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Ignore]
        public decimal Amount => Price * Quantity;
       
        //public object Item { get; internal set; }
    }
}
