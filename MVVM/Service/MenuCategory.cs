using SQLite;

namespace RestaurantPOS.Data
{
    // Represents a menu category in the SQLite database.
    public class MenuCategory
    {

        // Primary key for the MenuCategory table, automatically increments with each new entry.

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // The name of the menu category (e.g., "Beverages", "Main Course").
        public string Name { get; set; }


        // Icon associated with the menu category (could be a file path or URL).

        public string Icon { get; set; }
    }
}
