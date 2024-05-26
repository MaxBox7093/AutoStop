namespace AutoStop;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
    }

    private async void OnFindClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TripInfoPage());
    }

    private async void OnTripTapped(object sender, EventArgs e)
    { 
        await Navigation.PushAsync(new TripInfoPage()); 
    }

}