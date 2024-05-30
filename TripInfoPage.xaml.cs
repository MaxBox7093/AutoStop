using AutoStop.Storages;
using AutoStop.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoStop.APIServices;
using System.Diagnostics;

namespace AutoStop;

public partial class TripInfoPage : ContentPage
{
    GetNameByPhoneNumAPI _nameAPI;
    public TripInfoPage(Travel travel)
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

        // Изменяем вызов асинхронного метода
        LoadDriverNameAsync(travel.phoneDriver);
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
