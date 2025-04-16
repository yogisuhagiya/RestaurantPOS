using RestaurantPosMAUI.MVVM.ViewModels;

namespace RestaurantPosMAUI.MVVM.Pages;

public partial class SignUpPage : ContentPage
{
	private readonly SignUpViewModel _signUpViewModel;

    public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
        BindingContext = _signUpViewModel = signUpViewModel;
    }
}