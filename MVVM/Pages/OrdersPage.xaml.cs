using RestaurantPOS.ViewModels;


namespace RestaurantPOS.Pages;

// OrdersPage is the page responsible for displaying the list of orders

public partial class OrdersPage : ContentPage
{

    // Declaring a private field to hold the instance of OrdersViewModel

    private readonly OrdersViewModel _ordersViewModel;


    // Constructor accepts OrdersViewModel and initializes the component

    public OrdersPage(OrdersViewModel ordersViewModel)
	{

        // Initializes the UI components defined in the XAML file

        InitializeComponent();
        _ordersViewModel = ordersViewModel;
        // Assigns the OrdersViewModel passed into the constructor



        BindingContext = _ordersViewModel;
        // Sets the binding context to the OrdersViewModel to enable data binding


        InitializeViewModelAsync();
        // Asynchronously initializes the OrdersViewModel with any necessary data or setup

    }


    private async void InitializeViewModelAsync() => await _ordersViewModel.InitializeAsync();

}