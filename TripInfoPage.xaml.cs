using AutoStop.Storages;
using AutoStop.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoStop.APIServices;
using System.Diagnostics;

namespace AutoStop;

public partial class TripInfoPage : ContentPage
{
    int passc = 0;
    Travel travtemp;
    GetNameByPhoneNumAPI _nameAPI;
    TravelDeleteAPI _deleteAPI;
    TravelAddPassAPI _addAPI;
    public TripInfoPage(Travel travel, int pc)
    {
        _nameAPI = new GetNameByPhoneNumAPI();
        InitializeComponent();
        ViewModel vm = new ViewModel();
        if (UsersStorage.CurrentUser.Phone == travel.phoneDriver)
        {
            vm.IsDriver = true;
        }
        else
        {
            vm.IsDriver = false;
        }
        this.BindingContext = vm;

        DriverPhone.Text = travel.phoneDriver;
        From.Text = travel.startCity;
        To.Text = travel.endCity;
        TripDate.Text = DateOnly.FromDateTime((DateTime)travel.dateTime).ToString();
        PassCount.Text = travel.Passengers?.Count().ToString() ?? "0";
        Comment.Text = travel.comment;
        travtemp = travel;
        _deleteAPI = new TravelDeleteAPI();
        _addAPI = new TravelAddPassAPI();
        // Изменяем вызов асинхронного метода
        LoadDriverNameAsync(travel.phoneDriver);
        passc = pc;
    }

    private async void LoadDriverNameAsync(string ph)
    {
        DriverFullName.Text = await getName(ph);
    }

    private async Task<string> getName(string ph)
    {
        string res = await _nameAPI.GetName(ph);
        if (res == null)
        {
            return "User-data-were-corrupted";
        }
        else
        {
            return res;
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        deleteTripAsync(travtemp);
        await Navigation.PopAsync();
    }

    private void AddPassengerClicked(object sender, EventArgs e)
    {
        Passenger pass = new Passenger
        {
            PhonePassenger = UsersStorage.CurrentUser.Phone,
            IdTravel = travtemp.idTravel,
            NumberPassenger = passc
        };

        addPassToTripAsync(pass);
    }

    private void ToChatsClicked(object sender, EventArgs e)
    {
        
    }

    private async void deleteTripAsync(Travel travel)
    {
        bool success = await _deleteAPI.Delete(travel);
        if (success)
        {
            await DisplayAlert("Успех", "Поездка успешно удалена", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось удалить поездку", "OK");
        }
    }

    private async void addPassToTripAsync(Passenger passenger)
    {
        bool success = await _addAPI.Add(passenger);
        if (success)
        {
            await DisplayAlert("Успех", "Поездка успешно забронирована", "OK");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось забронировать поездку", "OK");
        }
    }
}

public class ViewModel : INotifyPropertyChanged
{
    private bool isDriver;

    public bool IsDriver
    {
        get => isDriver;
        set
        {
            if (isDriver != value)
            {
                isDriver = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
