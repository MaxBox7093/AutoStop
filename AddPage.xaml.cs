using AutoStop.APIServices;
using AutoStop.Models;
using AutoStop.Storages;
using AutoStopAPI.Models;

namespace AutoStop;

public partial class AddPage : ContentPage
{
    public List<Car> Cars { get; set; }
    public Car SelectedCar { get; set; }
    private readonly GetCarAPI _getCarAPI;
    private readonly AddTravelAPI _addTravelAPI;

    public AddPage()
    {
        InitializeComponent();
        _getCarAPI = new GetCarAPI();
        _addTravelAPI = new AddTravelAPI();
        Cars = new List<Car>();
        LoadCars();
        BindingContext = this;
    }

    private void AddTripClicked(object sender, EventArgs e)
    {
        var travel = new Travel
        {
            startCity = From.Text,
            endCity = To.Text,
            dateTime = TravelDate.Date,
            comment = Comment.Text,
            phoneDriver = UsersStorage.CurrentUser.Phone,
            carGRZ = SelectedCar?.GRZ
        };

        TravelsAdd(travel);
    }

    private async void LoadCars()
    {
        var cars = await _getCarAPI.GetCar(UsersStorage.CurrentUser.Phone);
        if (cars != null)
        {
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось загрузить автомобили", "OK");
        }
    }

    private async void TravelsAdd(Travel travel)
    {
        int? id = await _addTravelAPI.AddTravel(travel);
        if (id != null)
        {
            await DisplayAlert("Успех", "Поездка успешно создана", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось создать поездку", "OK");
        }
    }
}
