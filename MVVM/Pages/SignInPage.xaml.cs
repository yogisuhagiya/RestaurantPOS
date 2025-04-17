using RestaurantPosMAUI.MVVM.ViewModels;


// Namespace declaration for the SignInPage class within the RestaurantPosMAUI.MVVM.Pages namespace

namespace RestaurantPosMAUI.MVVM.Pages;


// SignInPage is a ContentPage that uses SignInViewModel for handling logic.

public partial class SignInPage : ContentPage
{
    // Private field to hold the SignInViewModel instance

    private readonly SignInViewModel _signInViewModel;

    // Constructor that takes SignInViewModel as a parameter

    public SignInPage(SignInViewModel signInViewModel)
	{

        // Initializes the components of the page

        InitializeComponent();

        // Sets the BindingContext for data-binding
        BindingContext = _signInViewModel = signInViewModel;
	}
}