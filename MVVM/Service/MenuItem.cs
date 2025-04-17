using SQLite;

namespace RestaurantPOS.Data
{

    // Represents a menu item in the SQLite database.

    public class MenuItem

    // Primary key for the MenuItem table, automatically increments with each new entry.

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Name of the menu item (e.g., "Pizza", "Burger").

        public string Name { get; set; }

        // Icon associated with the menu item (could be a file path or URL).

        public string Icon { get; set; }


        // A brief description of the menu item (e.g., "A tasty cheese pizza").

        public string Description { get; set; }


        // Price of the menu item (stored as a decimal for precision).

        public decimal Price { get; set; }
    }
}
