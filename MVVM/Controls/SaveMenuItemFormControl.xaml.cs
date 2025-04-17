using CommunityToolkit.Mvvm.Input;
using RestaurantPOS.Models;

namespace RestaurantPOS.Controls;

public partial class SaveMenuItemFormControl : ContentView
{
    // Default icon path for menu items

    private const string DefaultIcon = "image_add_regular_36.png";

    // Constructor to initialize the content view
    public SaveMenuItemFormControl()
    {
        InitializeComponent();
    }

    // Bindable property for MenuItemModel

    public static readonly BindableProperty ItemProperty = BindableProperty.Create(
       nameof(Item),
       typeof(MenuItemModel),
       typeof(SaveMenuItemFormControl),
       new MenuItemModel(),
       propertyChanged: OnItemChanged
   );

    // Public property to bind the menu item to the form control

    public MenuItemModel Item
    {
        get => (MenuItemModel)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    // RelayCommand to toggle category selection when clicked

    [RelayCommand]
    private void ToggleCategorySelection(MenuCategoryModel category) => category.IsSelected = !category.IsSelected;


    // Property changed callback for when the Item property changes
    private static void OnItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is MenuItemModel menuItemModel)
        {
            if (bindable is SaveMenuItemFormControl thisControl)
            {
                // If the menu item exists, set the icon image accordingly

                if (menuItemModel.Id > 0)
                {
                    thisControl.SetIconImage(isDefault: false, menuItemModel.Icon, thisControl);
                    thisControl.ExistingIcon = menuItemModel.Icon;
                }
                else
                {
                    thisControl.SetIconImage(isDefault: true, null, thisControl);
                }
            }
        }
    }

    // Existing icon for the menu item (to persist between changes)

    public string? ExistingIcon { get; set; }

    // Event triggered when Cancel button is pressed

    public event Action? OnCancel;

    // RelayCommand for the Cancel button

    [RelayCommand]
    private void Cancel() => OnCancel?.Invoke();

    // Async method for selecting an image to represent the menu item

    private async void PickImageButton_Clicked(object sender, EventArgs e)
    {
        var fileResult = await MediaPicker.PickPhotoAsync();

        // Open media picker to choose an image

        if (fileResult != null)
        {
            var imageStream = await fileResult.OpenReadAsync();

            var localPath = Path.Combine(FileSystem.AppDataDirectory, fileResult.FileName);

            // Save the selected image to the app's data directory

            using var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write);

            await imageStream.CopyToAsync(fs);

            // Set the selected image as the icon

            SetIconImage(isDefault: false, localPath);

            // Save the image path to the Item object

            Item.Icon = localPath;
        }
        else
        {
            // If no image was selected, set the existing icon or default icon

            if (ExistingIcon != null)
            {
                SetIconImage(isDefault: false, ExistingIcon);
            }
            else
            {
                SetIconImage(isDefault: true);
            }
        }
    }

    public void SetIconImage(bool isDefault, string? iconSource = null, SaveMenuItemFormControl? control = null)
    {
        int size = 100;
        if (isDefault)
        {
            iconSource = DefaultIcon;
            size = 36;
        }

        control = control ?? this;
        control.itemIcon.Source = iconSource;
        control.itemIcon.WidthRequest = control.itemIcon.HeightRequest = size;
    }

    public event Action<MenuItemModel> OnSaveItem;

    [RelayCommand]
    private async Task SaveMenuItemAsync()
    {
        // Validate the required fields: Name, Description, Categories, and Icon

        if (string.IsNullOrWhiteSpace(Item.Name) || string.IsNullOrWhiteSpace(Item.Description))
        {
            await ErrorAlertAsync("Item Name and Description are required");
            return;
        }

        if (Item.SelectedCategories.Length == 0)
        {
            await ErrorAlertAsync("Please select at least 1 category");
            return;
        }

        if (string.IsNullOrEmpty(Item.Icon) || Item.Icon == DefaultIcon)
        {
            await ErrorAlertAsync("Icon image is required");
            return;
        }

        // Trigger save action if all validations pass

        OnSaveItem?.Invoke(Item);

        // Helper method to show error alerts


        static async Task ErrorAlertAsync(string message) => await Shell.Current.DisplayAlert("Validation Error", message, "OK");
    }


}