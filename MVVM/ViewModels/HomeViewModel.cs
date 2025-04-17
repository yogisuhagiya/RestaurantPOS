using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantPOS.Data;
using RestaurantPOS.Models;
using System.Collections.ObjectModel;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.ViewModels
{

    // ViewModel for the Home Page - handles menu selection, cart operations, and order placement.

    public partial class HomeViewModel : ObservableObject, IRecipient<MenuItemChangedMessage>
    {
        private readonly DatabaseService _databaseService;
        private readonly OrdersViewModel _ordersViewModel;
        private readonly SettingsViewModel _settingsViewModel;


        // List of all available categories for menu filtering.

        [ObservableProperty]
        private MenuCategoryModel[] _categories = [];


        // List of menu items currently shown based on selected category.

        [ObservableProperty]
        private MenuItem[] _menuItems = [];

        // Currently selected category in the UI.

        [ObservableProperty]
        private MenuCategoryModel? _selectedCategory = null;


        // Items added to the cart by the user.

        public ObservableCollection<CartModel> CartItems { get; set; } = new();


        // Indicates if the UI is loading data.

        [ObservableProperty]
        private bool _isLoading;


        // Subtotal value of the current cart (before tax).

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TaxAmount)), NotifyPropertyChangedFor(nameof(Total))]
        private decimal _subtotal;

        // Tax percentage applied to the order.

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TaxAmount)), NotifyPropertyChangedFor(nameof(Total))]
        private int _taxPrecentage;


        // Calculates tax amount based on the subtotal and tax percentage.

        public decimal TaxAmount => (Subtotal * TaxPrecentage) / 100;

        // Total amount = Subtotal + Tax.

        public decimal Total => Subtotal + TaxAmount;

        [ObservableProperty]
        private string _name = "Guest";

        // Name of the user (displayed in UI).

        public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel, SettingsViewModel settingsViewModel)
        {
            _databaseService = databaseService;
            _ordersViewModel = ordersViewModel;
            _settingsViewModel = settingsViewModel;

            CartItems.CollectionChanged += (sender, args) => RecalculateAmounts();

            // Register to receive messages when a menu item is changed or user name is updated.

            WeakReferenceMessenger.Default.Register<MenuItemChangedMessage>(this);
            WeakReferenceMessenger.Default.Register<NameChangedMessage>(this, (reciepent, message) => Name = message.Value);

            TaxPrecentage = _settingsViewModel.GetTaxPercentage();
        }

        private bool _isInitialized;

        public async ValueTask InitializeAsync()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            IsLoading = true;

            Categories = (await _databaseService.GetMenuCategoriesAsync())
                            .Select(MenuCategoryModel.FromEntity)
                            .ToArray();

            Categories[0].IsSelected = true;
            SelectedCategory = Categories[0];
            // Load menu items for selected category.


            MenuItems = await _databaseService.GetMenuItemsByCategoryIdAsync(SelectedCategory.Id);

            IsLoading = false;
            // Handles category selection by the user.

        }

        [RelayCommand]
        private async Task SelectCategoryAsync(int categoryId)
        {
            if (SelectedCategory?.Id == categoryId)
                return; // Already selected

            IsLoading = true;

            // Unselect previous and select the new one.

            var currentSelectedCategory = Categories.First(c => c.IsSelected);
            currentSelectedCategory.IsSelected = false;

            var newSelectedCategory = Categories.First(c => c.Id == categoryId);
            newSelectedCategory.IsSelected = true;

            SelectedCategory = newSelectedCategory;

            // Load items for the selected category.

            MenuItems = await _databaseService.GetMenuItemsByCategoryIdAsync(SelectedCategory.Id);

            IsLoading = false;

            // Adds a menu item to the cart.

        }

        [RelayCommand]
        private void AddToCart(MenuItem menuItem)
        {
            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == menuItem.Id);
            if (cartItem == null)
            {
                cartItem = new CartModel()
                {
                    ItemId = menuItem.Id,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Icon = menuItem.Icon,
                    Quantity = 1
                };
                CartItems.Add(cartItem);
            }
            else
            {

                // Increase quantity if already in cart.

                cartItem.Quantity++;
                RecalculateAmounts();
            }
        }

        // Decrease item quantity or remove it if quantity reaches zero.


        [RelayCommand]
        private void IncreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity++;
            RecalculateAmounts();
        }

        // Remove item completely from cart.

        [RelayCommand]
        private void DecreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity--;
            if (cartItem.Quantity == 0)
            {
                CartItems.Remove(cartItem);
            }
            else
            {
                RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);


        // Recalculate subtotal when cart is updated.

        private void RecalculateAmounts() => Subtotal = CartItems.Sum(i => i.Amount);


        // Handle tax percentage change by user input.

        [RelayCommand]
        private async Task TaxPercentageClickAsync()
        {
            var result = await Shell.Current.DisplayPromptAsync("Tax Percentage", "Enter tax percentage", placeholder: "10", initialValue: TaxPrecentage.ToString());
            if (!string.IsNullOrWhiteSpace(result))
            {
                if (!int.TryParse(result, out int enteredTaxPercentage))
                {
                    await Shell.Current.DisplayAlert("Invalid value", "Please enter a valid number", "OK");
                    return;
                }

                if (enteredTaxPercentage > 100 || enteredTaxPercentage < 0)
                {
                    await Shell.Current.DisplayAlert("Invalid value", "Tax percentage must be between 0 and 100", "OK");
                    return;
                }

                TaxPrecentage = enteredTaxPercentage;

                _settingsViewModel.SetTaxPercentage(enteredTaxPercentage);
            }
        }

        // Place order and send cart to orders viewmodel.

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if (CartItems.Count > 0)
            {
                if (await Shell.Current.DisplayAlert("Clear Order", "Are you sure you want to clear the order?", "Yes", "No"))
                {
                    CartItems.Clear();
                }
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync(bool isPaidCash)
        {
            if (CartItems.Count == 0)
            {
                return;
            }

            if (await Shell.Current.DisplayAlert("Close Order", "Are you sure you want to close the order?", "Yes", "No"))
            {
                IsLoading = true;

                // Create order in OrdersViewModel and clear cart if successful.

                if (await _ordersViewModel.CreateOderAsync(CartItems.ToArray(), isPaidCash)) // Fix: Convert CartItems to an array using ToArray()
                {
                    CartItems.Clear();
                }
                IsLoading = false;
            }
        }

        // Receives real-time updates when menu items are changed.

        public void Receive(MenuItemChangedMessage message)
        {
            var model = message.Value;
            var menuItem = MenuItems.FirstOrDefault(m => m.Id == model.Id);
            if (menuItem != null)
            {
                // If menu item no longer belongs to selected category, remove it.

                if (!model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
                {
                    MenuItems = MenuItems.Where(m => m.Id != model.Id).ToArray(); // Fix: Use LINQ and convert to array
                    return;
                }
                // Update existing item details.

                menuItem.Name = model.Name;
                menuItem.Price = model.Price;
                menuItem.Description = model.Description;
                menuItem.Icon = model.Icon;

                MenuItems = MenuItems.ToArray(); // Fix: Convert to array
            }
            else if (model.SelectedCategories.Any(c => c.Id == SelectedCategory.Id))
            {
                // Add new item if it belongs to selected category.

                var newMenuItem = new MenuItem
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Icon = model.Icon
                };

                MenuItems = MenuItems.Append(newMenuItem).ToArray(); // Fix: Use Append and convert to array
            }

            var cartItem = CartItems.FirstOrDefault(i => i.ItemId == model.Id);
            if (cartItem != null)
            {
                cartItem.Name = model.Name;
                cartItem.Price = model.Price;
                cartItem.Icon = model.Icon;

                var itemIndex = CartItems.IndexOf(cartItem);

                CartItems[itemIndex] = cartItem; // No changes needed here
            }
        }
    }
}
