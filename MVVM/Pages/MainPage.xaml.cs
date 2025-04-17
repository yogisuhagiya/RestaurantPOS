using RestaurantPOS.ViewModels;
using RestaurantPosMAUI.MVVM.Service;
using System.Diagnostics;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly SettingsViewModel _settingsViewModel;


        private async void OnPayOnlineClicked(object sender, EventArgs e)
        {
            try
            {
                var paymentService = new PaymentService();

                // Get total from ViewModel and convert to cents

                long totalAmountInCents = 2121; // Example: $29.99 — replace this with your actual total
                var checkoutUrl = await paymentService.CreateCheckoutSessionAsync(totalAmountInCents);

                // Open the Stripe checkout page in the device browser
                await Launcher.Default.OpenAsync(new Uri(checkoutUrl));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Payment error: {ex.Message}");
                await DisplayAlert("Error", "Payment could not be initiated.", "OK");
            }
        }


        public MainPage(HomeViewModel homeViewModel, SettingsViewModel settingsViewModel)
        {
            InitializeComponent();

            _homeViewModel = homeViewModel;
            _settingsViewModel = settingsViewModel;

            BindingContext = _homeViewModel;

            Initialize();

        }

        private async void Initialize()
        {
            await _homeViewModel.InitializeAsync();
        }

        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            await _settingsViewModel.InitializeAsync();
        }

        private async void OnCategorySelected(Models.MenuCategoryModel category)
        {
            await _homeViewModel.SelectCategoryCommand.ExecuteAsync(category.Id);
        }

        private void OnItemSelected(MenuItem menuItem)
        {
            _homeViewModel.AddToCartCommand.Execute(menuItem);
        }
    }
}
