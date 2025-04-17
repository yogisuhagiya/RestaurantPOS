using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace RestaurantPOS.Models
{
    // Represents a menu item (e.g., Pizza, Coffee) in the restaurant POS system
    public partial class MenuItemModel : ObservableObject
    {
   
        // This will store the data under separate menu items in the menu.
         
        // Unique identifier for the menu item (used for editing or referencing)

        public int Id { get; set; }

        // Name of the menu item (e.g., "Margherita Pizza")

        [ObservableProperty]
        private string _name;

        // Price of the item (in decimal to support currency)

        [ObservableProperty]
        private decimal _price;

        // Path or reference to the icon/image representing the item
        [ObservableProperty]
        private string _icon;

        // Description of the item (e.g., ingredients or preparation notes)

        [ObservableProperty]
        private string _description;


        // All categories that this item can belong to
        public ObservableCollection<MenuCategoryModel> Categories { get; set; } = [];

        // Returns only the categories that have been selected by the user

        public MenuCategoryModel[] SelectedCategories => Categories.Where(c => c.IsSelected).ToArray();
    }
}
