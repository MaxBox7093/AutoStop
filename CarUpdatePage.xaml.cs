using AutoStop.Models;
using AutoStop.Storages;
using AutoStop.APIServices;

namespace AutoStop;

public partial class CarUpdatePage : ContentPage
{
	string oldgrz = "";

    UpdateCarAPI _updateCarAPI;

	public CarUpdatePage(Car car)
	{
		InitializeComponent();
		oldgrz = car.GRZ;
        _updateCarAPI = new UpdateCarAPI();

        newGRZ.Text = car.GRZ;
        newCarMod.Text = car.CarModel;
        newCarCol.Text = car.Color;
    }

    private async void OnUpdCarButtonClicked(object sender, EventArgs e)
    {
        if (newGRZ.Text != "" && oldgrz != "" && newCarMod.Text != "" && newCarCol.Text != "")
        { var car = new Car
            {
                GRZ = newGRZ.Text,
                OldGRZ = oldgrz,
                PhoneUser = UsersStorage.CurrentUser.Phone,
                CarModel = newCarMod.Text,
                Color = newCarCol.Text
            };

            UpdateCarAsync(car);
            await Shell.Current.GoToAsync("//ProfilePage");

        } else
        {
            await DisplayAlert("Ошибка", "Одно из полей было пустым", "OK");
        } 
    }

    private async void UpdateCarAsync(Car car)
    {
        bool success = await _updateCarAPI.UpdateCar(car);
        if (success)
        {
            await DisplayAlert("Успех", "Информация об автомобиле успешно изменена", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не изменить информацию о вашем автомобиле", "OK");
        }
    }
}