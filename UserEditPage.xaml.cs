using AutoStop.Models;
using AutoStop.APIServices;
using AutoStop.Storages;

namespace AutoStop;

public partial class UserEditPage : ContentPage
{
    readonly UpdateUserAPI _updateUserAPI;

    public UserEditPage(User user)
    {
        InitializeComponent();
        newName.Text = user.Name;
        newSecondName.Text = user.LastName;
        newDate.Date = user.DateOfBirth;
        _updateUserAPI = new UpdateUserAPI();
    }

    private async void OnAcceptButtonClicked(object sender, EventArgs e)
    {
        var pswd = UsersStorage.CurrentUser.Password;

        if (!string.IsNullOrEmpty(NewPassword.Text))
        {
            if (string.IsNullOrEmpty(CurrentPassword.Text) || CurrentPassword.Text != UsersStorage.CurrentUser.Password)
            {
                await DisplayAlert("������", "������� ������ �� ����� ��� ����� �������", "OK");
                return;
            }

            if (string.IsNullOrEmpty(DoubleNewPassword.Text))
            {
                await DisplayAlert("������", "��������� ����� ������ ��� ���", "OK");
                return;
            }

            if (NewPassword.Text != DoubleNewPassword.Text)
            {
                await DisplayAlert("������", "������ �� ���������", "OK");
                return;
            }

            pswd = NewPassword.Text;
        }

        User user = new User
        {
            Name = newName.Text,
            LastName = newSecondName.Text,
            DateOfBirth = newDate.Date,
            Phone = UsersStorage.CurrentUser.Phone,
            Password = pswd
        };

        await RegisterUserAsync(user);
        UsersStorage.CurrentUser = user;
        await Navigation.PushAsync(new ProfilePage());
    }

    private async Task RegisterUserAsync(User user)
    {
        bool success = await _updateUserAPI.UpdateUser(user);
        if (success)
        {
            await DisplayAlert("�����", "���������� ������ �������", "OK");
        }
        else
        {
            await DisplayAlert("������", "���������� �� �������", "OK");
        }
    }
}
