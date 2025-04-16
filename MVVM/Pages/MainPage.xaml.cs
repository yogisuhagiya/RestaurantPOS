using RestaurantPOS.ViewModels;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly SettingsViewModel _settingsViewModel;


        private async void OnPayClicked(object sender, EventArgs e)
        {
            var paymentUrl = "https://buy.stripe.com/test_aEU8A4ccz4kW6xa8ww"; // Replace with your Stripe link
            await Launcher.OpenAsync(new Uri(paymentUrl));
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
