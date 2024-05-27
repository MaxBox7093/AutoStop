using AutoStop.APIServices;
using AutoStop.Models;
using Microsoft.Maui.Controls.Handlers;

namespace AutoStop;

public partial class RegistrationCarPage : ContentPage
{
    string phonenum = "";

    private readonly RegistrationCarAPI _registrationCarAPI;

    public RegistrationCarPage()
	{
		InitializeComponent();
        _registrationCarAPI = new RegistrationCarAPI();
    }

    public RegistrationCarPage(string phonenum) : this()
    {
        this.phonenum = phonenum;
    }

    private async void OnAddCarButtonClicked(object sender, EventArgs e)
    {
        var car = new Car
        {
            GRZ = EntryGRZ.Text,
            PhoneUser = phonenum,
            CarModel = EntryCarMod.Text,
            Color = EntryCarCol.Text
        };

        RegisterCarAsync(car);

        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void RegisterCarAsync(Car car)
    {
        bool success = await _registrationCarAPI.RegisterCar(car);
        if (success)
        {
            await DisplayAlert("Успех", "Регистрация прошла успешно", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Регистрация не удалась", "OK");
        }
    }
}