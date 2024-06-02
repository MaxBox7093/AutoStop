using AutoStop.APIServices;
using AutoStop.Models;
using System.Collections.ObjectModel;

namespace AutoStop;

public partial class SearchPage : ContentPage
{
    public ObservableCollection<Travel> Travels { get; set; }
    private readonly searchTravelAPI _searchAPI;

    public SearchPage()
    {
        InitializeComponent();
        Travels = new ObservableCollection<Travel>();
        _searchAPI = new searchTravelAPI();
        BindingContext = this;
    }

    private async void OnFindClicked(object sender, EventArgs e)
    {
        var tripDate = TripDate.Date;
        var dateOnly = DateOnly.FromDateTime(tripDate);

        PassengerSearch ps = new PassengerSearch
        {
            startCity = From.Text,
            endCity = To.Text,
            numberPassenger = int.Parse(PassCountLabel.Text),
            date = dateOnly
        };

        var trips = await _searchAPI.GetTravels(ps);
        if (trips != null)
        {
            Travels.Clear();
            foreach (var trip in trips)
            {
                Travels.Add(trip);
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось найти поездки, удовлетворяющие критериям", "OK");
        }
    }

    private async void OnTravelSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedTravel = e.CurrentSelection.FirstOrDefault() as Travel;
        if (selectedTravel != null)
        {
            await Navigation.PushAsync(new TripInfoPage(selectedTravel, int.Parse(PassCountLabel.Text)));
        }
    }


    private void OnPassCountValueChanged(object sender, ValueChangedEventArgs e)
    {
        PassCountLabel.Text = e.NewValue.ToString();
    }
}
