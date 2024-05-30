using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoStop.Services;
using AutoStop.Models;

namespace AutoStop.APIServices
{
    internal class AddTravelAPI : ApiService
    {
        public async Task<int?> AddTravel(Travel travel)
        {
            try
            {
                // Преобразуем объект в JSON с использованием Newtonsoft.Json
                string json = JsonConvert.SerializeObject(travel);
                Console.WriteLine("JSON: " + json);  // Логирование JSON для отладки
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST-запрос
                HttpResponseMessage response = await _httpClient.PostAsync("Travel", content);

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем ответ
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response body: " + responseBody);  // Логирование ответа

                    // Преобразуем ответ в число
                    if (int.TryParse(responseBody, out int id))
                    {
                        return id;
                    }
                    else
                    {
                        Console.WriteLine("Error: Unable to parse response body to an integer.");
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
