using AutoStop.Services;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace AutoStop
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();

            _apiService = new ApiService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetUserId();
        }

        private async Task GetUserId()
        {
            try
            {
                int userId = await _apiService.GetUserIdAsync();
                EntryLogin.Text = $"User ID: {userId}";
            }
            catch (Exception ex)
            {
                EntryLogin.Text = $"Error: {ex.Message}";
            }
        }

        private void BtnLoginClick(object sender, EventArgs e)
        {
            // Логика для кнопки входа
        }

        private void BtnSignUpClick(object sender, EventArgs e)
        {
            // Логика для кнопки регистрации
        }
    }
}
