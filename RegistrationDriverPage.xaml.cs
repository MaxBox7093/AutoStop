using AutoStop.APIServices;
using AutoStop.Models;

namespace AutoStop;

public partial class RegistrationDriverPage : ContentPage
{
    string phonenum = "";

    private readonly RegistrationDriverAPI _registrationDriverAPI;

    public RegistrationDriverPage()
	{
		InitializeComponent();
        _registrationDriverAPI = new RegistrationDriverAPI();
    }

    public RegistrationDriverPage(string phonenum) : this()
    {
        this.phonenum = phonenum;
    }

    private async void OnAddDocButtonClicked(object sender, EventArgs e)
    {
        var driver = new Driver
        {
            PhoneNumber = phonenum,
            dateGetDoc = DateOnly.FromDateTime(EntryCarPlate.Date)
        };
        RegisterDriverAsync(driver);
        await Navigation.PushAsync(new RegistrationCarPage(phonenum));
    }

    private async void RegisterDriverAsync(Driver driver)
    {
        bool success = await _registrationDriverAPI.RegisterDriver(driver);
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