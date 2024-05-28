using AutoStop.APIServices;
using AutoStop.Models;
using AutoStop.Storages;

namespace AutoStop;

public partial class CarAddPage : ContentPage
{

    private readonly RegistrationCarAPI _registrationCarAPI;

    public CarAddPage()
	{
		InitializeComponent();
        _registrationCarAPI = new RegistrationCarAPI();

    }

    private async void OnAddCarButtonClicked(object sender, EventArgs e)
    {
        var car = new Car
        {
            GRZ = GRZ.Text,
            PhoneUser = UsersStorage.CurrentUser.Phone,
            CarModel = CarMod.Text,
            Color = CarCol.Text
        };

        RegisterCarAsync(car);

        await Shell.Current.GoToAsync("//ProfilePage");
    }

    private async void RegisterCarAsync(Car car)
    {
        bool success = await _registrationCarAPI.RegisterCar(car);
        if (success)
        {
            await DisplayAlert("Успех", "Автомобиль успешно добавлен", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось добавить ваш автомобиль", "OK");
        }
    }
}