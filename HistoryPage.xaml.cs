using AutoStop.APIServices;
using AutoStop.Models;
using AutoStop.Storages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoStop;

public partial class HistoryPage : ContentPage
{
    public ObservableCollection<Travel> Travels { get; set; }
    public ObservableCollection<Travel> InactiveTravels { get; set; }
    GetTravelsHistoryAPI _getTravelsHistoryAPI;
    CheckIsDriverAPI _drivercheckAPI;

    public HistoryPage()
    {
        InitializeComponent();
        Travels = new ObservableCollection<Travel>();
        InactiveTravels = new ObservableCollection<Travel>();
        BindingContext = this;

        btnchngclr("pass");
        _getTravelsHistoryAPI = new GetTravelsHistoryAPI();
        _drivercheckAPI = new CheckIsDriverAPI();
        DrvChck();
        LoadTravels(false);
    }

    private void PassButton_Clicked(object sender, EventArgs e)
    {
        btnchngclr("pass");
        LoadTravels(false);
    }

    private void DriverButton_Clicked(object sender, EventArgs e)
    {
        btnchngclr("driver");
        LoadTravels(true);
    }

    private void btnchngclr(string state)
    {
        if (state == "pass")
        {
            PassButton.BackgroundColor = Colors.Orange;
            PassButton.TextColor = Colors.White;
            DriverButton.BackgroundColor = Color.FromArgb("#2196F3");
            DriverButton.TextColor = Colors.LightBlue;
        }
        else if (state == "driver")
        {
            DriverButton.BackgroundColor = Colors.Orange;
            DriverButton.TextColor = Colors.White;
            PassButton.BackgroundColor = Color.FromArgb("#2196F3");
            PassButton.TextColor = Colors.LightBlue;
        }
    }

    private async void DrvChck()
    {
        RoleSelector.IsVisible = await OnDriverChecked(UsersStorage.CurrentUser.Phone);
    }

    private async Task<bool> OnDriverChecked(string phone)
    {
        bool success = await _drivercheckAPI.Check(phone);
        return success;
    }

    private async void LoadTravels(bool isDriver)
    {
        Travels.Clear();
        InactiveTravels.Clear();
        var travels = await _getTravelsHistoryAPI.Get(UsersStorage.CurrentUser.Phone, isDriver);
        if (travels != null)
        {
            var sortedTravels = travels.OrderByDescending(t => t.dateTime).ToList();
            foreach (var travel in sortedTravels)
            {
                if (travel.isActive == true)
                {
                    Travels.Add(travel);
                }
                else
                {
                    InactiveTravels.Add(travel);
                }
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Поездки не найдены", "OK");
        }
    }
}
