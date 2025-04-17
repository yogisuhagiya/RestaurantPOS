using CommunityToolkit.Mvvm.Messaging;
using RestaurantPOS.Models;

namespace RestaurantPOS.ViewModels
{
    // ViewModel for handling settings like user's name and tax percentage

    public class SettingsViewModel
    {
        private const string NameKey = "name";
        private const string TaxPercentageKey = "tax";

        private bool _isInitialized;

        // Initializes the settings view, prompting the user for their name if not set

        public async ValueTask InitializeAsync()
        {
            if (_isInitialized) return;

            _isInitialized = true;

            // Retrieve the stored name from preferences

            var name = Preferences.Default.Get<string?>(NameKey, null);

            // If the name is not set, prompt the user for it

            if (name == null)
            {
                do
                {
                    name = await Shell.Current.DisplayPromptAsync("Your name", "Enter your name");
                } while (string.IsNullOrWhiteSpace(name));
                // Ensure the user enters a valid name


                // Save the name to preferences for future use

                Preferences.Default.Set<string>(NameKey, name);

            }
            // Send a message indicating the name has been changed or set

            WeakReferenceMessenger.Default.Send(NameChangedMessage.From(name));
        }

        // Retrieves the stored tax percentage from preferences

        public int GetTaxPercentage() => Preferences.Default.Get<int>(TaxPercentageKey, 0);

        public void SetTaxPercentage(int taxPercentage) => Preferences.Default.Set<int>(TaxPercentageKey, taxPercentage);

    }
}
