using AutoStop.APIServices;
using AutoStop.Storages;

namespace AutoStop;

public partial class ProfilePage : ContentPage
{
    CheckIsDriverAPI _drivercheckAPI;

    public ProfilePage()
	{
        InitializeComponent();
        username.Text = UsersStorage.CurrentUser.LastName + " " + UsersStorage.CurrentUser.Name;
        userbirthdate.Text = UsersStorage.CurrentUser.DateOfBirth.Date.ToString("dd.MM.yyyy");
        userphone.Text = "+7" + UsersStorage.CurrentUser.Phone;
        _drivercheckAPI = new CheckIsDriverAPI();
    }

    public ProfilePage(Models.User user)
    {
        InitializeComponent();
        UsersStorage.CurrentUser = user;
        username.Text = user.LastName + " " + user.Name;
        userbirthdate.Text = user.DateOfBirth.ToString("dd.MM.yyyy");
        userphone.Text = "+7" + user.Phone;
        _drivercheckAPI = new CheckIsDriverAPI();
    }

    private async void OnUserEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserEditPage(UsersStorage.CurrentUser));
    }

    private async void ViewAutoClicked(object sender, EventArgs e)
    {
        bool tmp = await OnDriverChecked(UsersStorage.CurrentUser.Phone);
        if (tmp) 
        {
            await Navigation.PushAsync(new ViewAutoPage());
        }
        else
        {
            await DisplayAlert("Ошибка", "Эта функция доступна только пользователям с учётной записью водителя", "OK");
        }
    }

    private async Task<bool> OnDriverChecked(string phone)
    {
        bool success = await _drivercheckAPI.Check(phone);

        return success;
    }
}