namespace RestaurantPOS.Controls;

//A custom ContentView that displays the current time and date,
// updating every second using a PeriodicTimer.

public partial class CurrentDateTimeControl : ContentView
{
    //// Timer that ticks every second to update time and date labels

    private readonly PeriodicTimer _timer;

    
    public CurrentDateTimeControl()
	{
		InitializeComponent(); // Initialize the XAML components

        _timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        //// Start continuously updating the labels with the current time/date

        UpdateTimeLabelEachSecond();
    }

    // set the time label to the current time

    private void UpdateTimeLabel()
    {
        DateTime now = DateTime.Now;

        string formattedTime = $"{now:dddd, HH:mm:ss}"; //  Format the time
        dayTimeLabel.Text = char.ToUpper(formattedTime[0]) + formattedTime.Substring(1);    //  Capitalize the first letter 

        string formattedDate = $"{now:dd MMMM yyyy}";
        formattedDate = formattedDate.Substring(0, 3) + char.ToUpper(formattedDate[3]) + formattedDate.Substring(4); // Capitalize the first letter of the month
        dateLabel.Text = formattedDate;
    }

    private async void UpdateTimeLabelEachSecond() 
    {
        //  Update the time label every second
        //  This is a blocking call, so it will not return until the timer is disposed
        //  or the application is closed    
        while (await _timer.WaitForNextTickAsync())
        {
            UpdateTimeLabel();
        }
    }
}