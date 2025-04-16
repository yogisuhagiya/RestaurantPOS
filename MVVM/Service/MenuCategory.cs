using SQLite;

namespace RestaurantPOS.Data
{
    // This will be fetch the menu Category in Sqlite database.
    public class MenuCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
