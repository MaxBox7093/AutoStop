using AutoStop.Storages;

namespace AutoStop;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
        InitializeComponent();
        username.Text = UsersStorage.CurrentUser.LastName + " " + UsersStorage.CurrentUser.Name;
        userbirthdate.Text = UsersStorage.CurrentUser.DateOfBirth.Date.ToString("dd.MM.yyyy");
        userphone.Text = "+7" + UsersStorage.CurrentUser.Phone;
    }

    public ProfilePage(Models.User user)
    {
        InitializeComponent();
        UsersStorage.CurrentUser = user;
        username.Text = user.LastName + " " + user.Name;
        userbirthdate.Text = user.DateOfBirth.ToString("dd.MM.yyyy");

        userphone.Text = "+7" + user.Phone;
    }

    private async void OnUserEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserEditPage(UsersStorage.CurrentUser));
    }

    private async void ViewAutoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewAutoPage());
    }
}