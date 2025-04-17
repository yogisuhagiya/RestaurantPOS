using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPOS.Data;

namespace RestaurantPOS.Models
{

    // Represents a category of menu items (e.g., Beverages, Snacks)

    public partial class MenuCategoryModel : ObservableObject
    {

        // Unique identifier for the category

        public int Id { get; set; }

        // Display name of the category
        public string Name { get; set; }

        // Icon representing the category
        public string Icon { get; set; }

        // Indicates whether the category is selected (e.g., in a form UI)

        [ObservableProperty]
        private bool _isSelected;

        // Converts a database entity to the model used in the UI
        public static MenuCategoryModel FromEntity(MenuCategory entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Icon = entity.Icon
        };
    }
}
