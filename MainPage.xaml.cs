using AutoStop.APIServices;
using AutoStop.Models;
using AutoStop.Services;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace AutoStop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnLoginClick(object sender, EventArgs e)
        {
            var login = new Login
            {
                Phone = EntryLogin.Text,
                Password = EntryPassword.Text
            };
            LoginAPI loginAPI = new LoginAPI();
            
            //Get и получение User
            User user = await loginAPI.LoginAsync(login);

            if (user != null)
            {
                await DisplayAlert("Уведомление", $"Добро пожаловать, {user.Name} {user.LastName}", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Логин или пароль неверны", "OK");
            }
            //Переход на профиль(Потом добавить в if!!!!)
            Navigation.PushAsync(new ProfilePage());
        }

        private void BtnSignUpClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }
    }
}
