using AutoStop.APIServices;
using AutoStop.Storages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AutoStop
{
    public partial class ProfilePage : ContentPage
    {
        CheckIsDriverAPI _drivercheckAPI;
        PatchImageAPI _patchImageAPI;

        public ProfilePage()
        {
            InitializeComponent();
            username.Text = UsersStorage.CurrentUser.LastName + " " + UsersStorage.CurrentUser.Name;
            userbirthdate.Text = UsersStorage.CurrentUser.DateOfBirth.Date.ToString("dd.MM.yyyy");
            userphone.Text = "+7" + UsersStorage.CurrentUser.Phone;
            _drivercheckAPI = new CheckIsDriverAPI();
            _patchImageAPI = new PatchImageAPI();
            LoadUserImage();
        }

        public ProfilePage(Models.User user)
        {
            InitializeComponent();
            UsersStorage.CurrentUser = user;
            username.Text = user.LastName + " " + user.Name;
            userbirthdate.Text = user.DateOfBirth.ToString("dd.MM.yyyy");
            userphone.Text = "+7" + user.Phone;
            _drivercheckAPI = new CheckIsDriverAPI();
            _patchImageAPI = new PatchImageAPI();
            LoadUserImage();
        }

        private async void LoadUserImage()
        {
            var stream = await _patchImageAPI.GetImageAsync(UsersStorage.CurrentUser.Phone);
            if (stream != null)
            {
                UserImage.Source = ImageSource.FromStream(() => stream);
            }
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

        private async void OnChangePhotoClicked(object sender, EventArgs e)
        {
            await PickAndShowPhotoAsync();
        }

        private async Task PickAndShowPhotoAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Выберите фотографию"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    UserImage.Source = ImageSource.FromStream(() => stream);
                    await UploadPhotoAsync(result.FullPath);
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                await DisplayAlert("Ошибка", "Не удалось загрузить фотографию: " + ex.Message, "OK");
            }
        }

        private async Task UploadPhotoAsync(string filePath)
        {
            try
            {
                using (var stream = File.OpenRead(filePath))
                {
                    bool success = await _patchImageAPI.UpdateImageAsync(UsersStorage.CurrentUser.Phone, stream);
                    if (success)
                    {
                        await DisplayAlert("Успех", "Фотография успешно обновлена.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Ошибка", "Не удалось обновить фотографию.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Не удалось отправить фотографию: " + ex.Message, "OK");
            }
        }
    }
}
