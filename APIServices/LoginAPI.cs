using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AutoStop.APIServices
{
    internal class LoginAPI : ApiService
    {
        public async Task<User?> LoginAsync(Login login)
        {
            try
            {
                // Сериализуем модель Login в JSON
                string json = JsonSerializer.Serialize(login);

                // Создаем HttpContent для POST-запроса
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST-запрос
                HttpResponseMessage response = await _httpClient.PostAsync("login", content);

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
