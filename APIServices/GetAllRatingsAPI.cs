using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class GetAllRatingsAPI : ApiService
    {
        public async Task<List<Rating>> GetRatings(string ph)
        {
            try
            {
                // Отправляем GET-запрос
                HttpResponseMessage response = await _httpClient.GetAsync($"rating?phone={ph}");

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Логирование содержимого ответа
                    Console.WriteLine("Response body: " + responseBody);

                    // Десериализация строки JSON в список объектов Car
                    List<Rating> ratings = JsonConvert.DeserializeObject<List<Rating>>(responseBody);

                    return ratings;
                }
                else
                {
                    return null;
                }
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
