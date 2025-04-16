using RestaurantPOS.Data;
using RestaurantPosMAUI.MVVM.Pages;
using RestaurantPosMAUI.MVVM.Service;

namespace RestaurantPOS
{
    public partial class App : Application
    {

        public App(DatabaseService databaseService)
        {
            InitializeComponent(); 
            DependencyService.Register<DatabaseService>();

            MainPage = new AppShell();


            Task.Run(async () => await databaseService.InitializeDatabase())
                .GetAwaiter()
                .GetResult();
        }
      

        private async Task<string> GetUserRole(string localId)
        {
            // Use Firebase Realtime Database or Firestore to fetch the user role by localId
            // Implement the logic for fetching the role from your Firebase Realtime Database
            string role = "staff"; // Default role, modify according to your implementation

            // Fetch role from your Firebase Realtime Database or Firestore
            // Example (simplified):
            // role = await FirebaseService.GetUserRole(localId);

            return role;
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.MinimumHeight = 760;
            window.MinimumWidth = 1280;
            return window;
        }
    }
}
