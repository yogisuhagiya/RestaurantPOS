using RestaurantPosMAUI.MVVM.ViewModels;

// Namespace declaration for the SignUpPage class within the RestaurantPosMAUI.MVVM.Pages namespace

namespace RestaurantPosMAUI.MVVM.Pages;


// SignUpPage is a ContentPage that uses SignUpViewModel for handling sign-up logic.

public partial class SignUpPage : ContentPage
{

    // Private field to hold the SignUpViewModel instance

    private readonly SignUpViewModel _signUpViewModel;

    public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
        BindingContext = _signUpViewModel = signUpViewModel;
        // Sets the BindingContext for data-binding

    }
}