using CommunityToolkit.Mvvm.Input;
using RestaurantPOS.Models;

namespace RestaurantPOS.Controls;

// This file is part of the RestaurantPOS project.
//This control exposes a bindable Categories property and a command to handle category selection.
public partial class CategoriesListControl : ContentView
{
    public CategoriesListControl()
    {
        InitializeComponent();
    }
    //Bindable property for passing a list of menu categories to the control.
    // This allows data binding from a parent view or view model.


    public static readonly BindableProperty CategoriesProperty = BindableProperty.Create(
        nameof(Categories),
        typeof(MenuCategoryModel[]),
        typeof(CategoriesListControl),
        Array.Empty<MenuCategoryModel>()
    );
    //Property wrapper for Categories bindable property.
    // Represents the list of menu categories displayed in the control.

    public MenuCategoryModel[] Categories
    {
        get => (MenuCategoryModel[])GetValue(CategoriesProperty);
        set => SetValue(CategoriesProperty, value);
    }

    public event Action<MenuCategoryModel> OnCategorySelected;

    [RelayCommand]
    private void SelectCategory(MenuCategoryModel category) => OnCategorySelected?.Invoke(category);
}