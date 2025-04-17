using CommunityToolkit.Mvvm.Input;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Controls;

public partial class MenuItemsListControl : ContentView
{
    public MenuItemsListControl()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(MenuItem[]),
        typeof(MenuItemsListControl),
        Array.Empty<MenuItem>()
    );

    // Getter and setter for the bindable Items property

    public MenuItem[] Items
    {
        get => (MenuItem[])GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    // Event triggered when a menu item is selected

    public event Action<MenuItem> OnItemSelected;

    // RelayCommand to handle item selection and trigger the event

    [RelayCommand]
    private void ItemSelected(MenuItem item) => OnItemSelected?.Invoke(item);

    // Icon shown in the UI, default is shopping bag

    public string ActionIcon { get; set; } = "shopping_bag.png";

    // Switch icon depending on editing mode

    public bool IsEditingMode { set => ActionIcon = (value ? "edit_solid_24.png" : "shopping_bag.png"); }
}