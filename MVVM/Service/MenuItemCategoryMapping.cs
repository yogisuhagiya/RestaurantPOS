using SQLite;

namespace RestaurantPOS.Data
{
    // This will be fetch the menu item category mapping in Sqlite database.
    public class MenuItemCategoryMapping
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int CategoryId { get; set; }
    }
}
