using AutoStop.Models;
using System.Text.Json;
using System.Text;

namespace AutoStop;

public partial class RegistrationPage : ContentPage
{
    private static readonly HttpClient httpClient = new HttpClient();

    public RegistrationPage()
	{
		InitializeComponent();
    }

    private void BtnSignUpRegClick(object sender, EventArgs e)
    {
        var registration = new Registration
        {
            Name = UserName.Text,
            LastName = UserSurename.Text,
            DateOfBirth = UserBirthday.Date,
            Phone = Convert.ToInt32(EntryLoginReg.Text),
            Password = EntryPasswordReg.Text
        };

        RegisterUser(registration);
    }

    private async void RegisterUser(Registration registration) 
    {
        try
        {
            // Преобразуем объект в JSON
            string json = JsonSerializer.Serialize(registration);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Отправляем POST-запрос
            HttpResponseMessage response = await httpClient.PostAsync("http://192.168.0.105:5083/api/registration", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Регистрация прошла успешно", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", "Регистрация не удалась", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка: {ex.Message}", "OK");
        }
    }
}