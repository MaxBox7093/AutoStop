using AutoStop.Models;
using AutoStop.APIServices;
using System;

namespace AutoStop
{
    public partial class RegistrationPage : ContentPage
    {
        private readonly RegistrationAPI _registrationAPI;

        public RegistrationPage()
        {
            InitializeComponent();
            _registrationAPI = new RegistrationAPI();
        }

        private void BtnSignUpRegClick(object sender, EventArgs e)
        {
            var registration = new Registration
            {
                Name = UserName.Text,
                LastName = UserSurename.Text,
                DateOfBirth = UserBirthday.Date,
                Phone = EntryLoginReg.Text,
                Password = EntryPasswordReg.Text
            };

            RegisterUserAsync(registration);
        }

        private async void RegisterUserAsync(Registration registration)
        {
            bool success = await _registrationAPI.RegisterUser(registration);
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
}
