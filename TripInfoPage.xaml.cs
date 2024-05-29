using AutoStop.Storages;
using AutoStopAPI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoStop;

public partial class TripInfoPage : ContentPage
{
    public TripInfoPage(Travel travel)
    {
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
        PassCount.Text = travel.Passengers.Count().ToString();
        Comment.Text = travel.comment;
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
