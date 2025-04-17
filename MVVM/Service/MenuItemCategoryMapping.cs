using SQLite;

namespace RestaurantPOS.Data
{
    // Represents the mapping between menu items and their categories in the SQLite database.
    public class MenuItemCategoryMapping
    {

        // Primary key for the MenuItemCategoryMapping table, automatically increments with each new entry.

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Foreign key referring to the associated MenuItem.

        public int MenuItemId { get; set; }

        // Foreign key referring to the associated MenuCategory.

        public int CategoryId { get; set; }
    }
}
