using CommunityToolkit.Maui.Views;
using RestaurantPOS.Controls;
using RestaurantPOS.Pages;
using RestaurantPosMAUI.MVVM.Pages;

namespace RestaurantPOS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(RestaurantPOS.Pages.MainPage)); 


        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var helpPopup = new HelpPopup();
            await this.ShowPopupAsync(helpPopup);
        }
    }
}
