using CommunityToolkit.Maui.Views;

namespace RestaurantPOS.Controls;

// HelpPopup: Custom popup displaying contact info and support options


public partial class HelpPopup : Popup
{
    // Static email address used throughout the popup

    public const string Email = "yogisuhagiya2002@gmail.com";

    // Email subject line and website URL
    private const string Subject = "Restaurant POS";
    private const string Website = "https://github.com/yogisuhagiya";

    public HelpPopup()
    {
        InitializeComponent();
        // Load the XAML layout
    }
    // Close the popup when the 'X' label is tapped
    private async void CloseLabel_Tapped(object sender, TappedEventArgs e) => await this.CloseAsync();


    // Open the user's default email app with a pre-filled subject when the email label is tapped
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.Default.OpenAsync("mailto:" + Email + "?subject=" + Subject);
    }

    // Copy email to clipboard and show visual confirmation (icon changes temporarily)
    private async void CopyEmail_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync(Email);

        CopyEmailImage.Source = "check.png";
        await Task.Delay(2000);
        CopyEmailImage.Source = "copy.png";
    }

    // Open the developer's website when the footer area is tapped
    private async void Footer_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.Default.OpenAsync(Website);
    }

}