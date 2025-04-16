using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using RestaurantPOS.Controls;
using RestaurantPOS.Data;
using RestaurantPOS.Pages;
using RestaurantPOS.ViewModels;
using RestaurantPosMAUI.MVVM.Pages;
using RestaurantPosMAUI.MVVM.Service;
using RestaurantPosMAUI.MVVM.ViewModels;


namespace RestaurantPOS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                });

          #if DEBUG
            builder.Logging.AddDebug();

#endif
            builder.Services.AddSingleton(new Firebase.Database.FirebaseClient("https://restaurantpos-6bc8a-default-rtdb.firebaseio.com"));
            builder.Services
                .AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
                {
                    ApiKey = "AIzaSyBh4bnGBdpDrqDV5PidiXIKowLm-GCUxwg\r\n",
                    AuthDomain = "restaurantpos-6bc8a.firebaseapp.com\t",
                    Providers = [new EmailProvider()]

                }))
                .AddSingleton<DatabaseService>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<OrdersViewModel>()
                .AddSingleton<OrdersPage>()
                .AddTransient<ManageMenuItemsViewModel>()
                .AddTransient<ManageMenuItemPage>()
                .AddSingleton<SettingsViewModel>()
                .AddSingleton<PdfGenerationService>()
                .AddSingleton<SignInViewModel>()
                .AddSingleton<SignUpViewModel>()
        
                .AddSingleton<SignInPage>()
                .AddSingleton<SignUpPage>()
                .AddSingleton<HelpPopup>();

            return builder.Build();
        }
    }
}
