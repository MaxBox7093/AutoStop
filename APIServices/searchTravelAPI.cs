using AutoStop.Models;
using AutoStop.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoStop.APIServices
{
    internal class searchTravelAPI : ApiService
    {
        public async Task<List<Travel>> GetTravels(PassengerSearch ps)
        {
            try
            {
                // Преобразуем объект PassengerSearch в строку JSON
                string jsonPs = JsonConvert.SerializeObject(ps);

                // Создаем контент для запроса
                var content = new StringContent(jsonPs, System.Text.Encoding.UTF8, "application/json");

                // Создаем запрос
                var request = new HttpRequestMessage(HttpMethod.Get, "searchTravel")
                {
                    Content = content
                };

                // Отправляем запрос
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Логирование статуса ответа
                Console.WriteLine("Response status code: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа как строку
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Логирование содержимого ответа
                    Console.WriteLine("Response body: " + responseBody);

                    // Десериализация строки JSON в список объектов Travel
                    List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(responseBody);

                    return travels;
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
