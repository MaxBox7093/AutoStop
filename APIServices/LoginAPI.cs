using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoStop.APIServices
{
    internal class LoginAPI : ApiService
    {
        public async Task<User?> LoginAsync(Login login)
        {
            try
            {
                // Формируем строку запроса с параметрами
                string requestUri = $"login?Phone={Uri.EscapeDataString(login.Phone)}&Password={Uri.EscapeDataString(login.Password)}";

                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем и десериализуем ответ
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    User? user = JsonSerializer.Deserialize<User>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return user;
                }

                return null;
            }
            catch
            {
                // Можно добавить логирование ошибок здесь
                return null;
            }
        }
    }
}
