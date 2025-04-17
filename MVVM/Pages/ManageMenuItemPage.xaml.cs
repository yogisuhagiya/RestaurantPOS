using RestaurantPOS.ViewModels;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Pages;

public partial class ManageMenuItemPage : ContentPage
{

    // The ViewModel for managing menu items.

    private readonly ManageMenuItemsViewModel _manageMenuItemViewModel;


    // Constructor: Initializes the page with the given ViewModel and sets the BindingContext.

    public ManageMenuItemPage(ManageMenuItemsViewModel manageMenuItemViewModel)
    {
        InitializeComponent();
        _manageMenuItemViewModel = manageMenuItemViewModel;
        BindingContext = _manageMenuItemViewModel;
        InitializeAsync();
    }

    // InitializeAsync method: Calls the ViewModel's InitializeAsync method to load necessary data.

    private async void InitializeAsync()
    {
        await _manageMenuItemViewModel.InitializeAsync();
    }


    // OnCategorySelected method: Executes the SelectCategoryCommand in the ViewModel when a category is selected.

    private async void OnCategorySelected(Models.MenuCategoryModel category) => await _manageMenuItemViewModel.SelectCategoryCommand.ExecuteAsync(category.Id);

    private async void OnItemSelected(MenuItem menuItem) => await _manageMenuItemViewModel.EditMenuItemCommand.ExecuteAsync(menuItem);


    // OnItemSelected method: Executes the EditMenuItemCommand in the ViewModel when a menu item is selected for editing.

    private void SaveMenuItemFormControl_OnCancel()
    {
        _manageMenuItemViewModel.CancelCommand.Execute(null);
    }

    // SaveMenuItemFormControl_OnSaveItem method: Executes the SaveMenuItemCommand to save a new or edited menu item.

    private async void SaveMenuItemFormControl_OnSaveItem(Models.MenuItemModel menuItemModel)
    {
        await _manageMenuItemViewModel.SaveMenuItemCommand.ExecuteAsync(menuItemModel);
    }
}